import axios from "axios";

const gqlURL = "http://localhost:5082/graphql";
const filterURL = "http://localhost:5082/Filter/GetAllFilterAsync";
const saveFilterURL = "http://localhost:5082/Filter/SaveQuery";
const deleteFilterURL = "http://localhost:5082/Filter/DeleteFilterAsync";
const excelURL = "http://localhost:5082/Report/GenerateExcel";

const formatDateStart = (d) => d ? `${d}T00:00:00Z` : null;
const formatDateEnd = (d) => d ? `${d}T23:59:59Z` : null;

const getUserId = () => "dc7a68d3-1072-42b1-8f74-ef062b2257e6";

axios.interceptors.request.use((config) => {
  config.headers['userId'] = getUserId();
  return config;
});

function buildGraphQLQuery(filters) {
  const params = [];
  if (filters.category) params.push(`category: "${filters.category}"`);
  if (filters.dateFrom) params.push(`from: "${formatDateStart(filters.dateFrom)}"`);
  if (filters.dateTo) params.push(`to: "${formatDateEnd(filters.dateTo)}"`);
  return `{
    donationSummary(
      ${params.join('\n      ')}
    ) {
      category
      totalQuantity
    }
  }`;
}

function buildFilterStringForSave(filters) {
  const params = [];
  if (filters.category) params.push(`category: "${filters.category}"`);
  if (filters.dateFrom) params.push(`from: "${formatDateStart(filters.dateFrom)}"`);
  if (filters.dateTo) params.push(`to: "${formatDateEnd(filters.dateTo)}"`);
  return `{ donationSummary(
    ${params.join('\n    ')}
  ) {
    category
    isCancelled
    totalQuantity
    items {
      requestId
      donationOrganizationId
      createdAt
      quantity
    }
  } }`;
}

export const donationReportService = {
  async getReport(filtersOrQuery) {
    const query = typeof filtersOrQuery === "string"
      ? filtersOrQuery
      : buildGraphQLQuery(filtersOrQuery);
    const res = await axios.post(gqlURL, { query });
    return res.data?.data?.donationSummary || [];
  },

  async getSavedFilters() {
    const res = await axios.get(filterURL);
    if (Array.isArray(res.data.data)) return res.data.data;
    if (Array.isArray(res.data.data?.filters)) return res.data.data.filters;
    return [];
  },

  async saveFilter({ name, filters }) {
    const filterString = buildFilterStringForSave(filters);
    await axios.post(saveFilterURL, {
      name,
      filter: filterString,
      type: "Donation"
    });
  },

  // EDIT = SAVE (el backend sobreescribe si el name existe)
  async editFilter({ name, filters }) {
    const filterString = buildFilterStringForSave(filters);
    await axios.post(saveFilterURL, {
      name,
      filter: filterString,
      type: "Donation"
    });
  },

  // DELETE: envía name como query param y UserId como header
  async deleteFilter(name) {
    await axios.delete(`${deleteFilterURL}?name=${encodeURIComponent(name)}`, {
      headers: {
        userId: getUserId(),
      },
    });
  },

  async downloadExcel(filters) {
    const res = await axios.post(excelURL, filters, {
      responseType: "blob"
    });
    const url = window.URL.createObjectURL(new Blob([res.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", "donaciones.xlsx");
    document.body.appendChild(link);
    link.click();
    link.parentNode.removeChild(link);
  }
};