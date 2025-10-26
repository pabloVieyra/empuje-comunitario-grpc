import axios from "axios";

const gqlURL = "http://localhost:5082/graphql";
const filterURL = "http://localhost:5082/Filter/GetAllFilterAsync";
const saveFilterURL = "http://localhost:5082/Filter/SaveQuery";
const deleteFilterURL = "http://localhost:5082/Filter/DeleteFilterAsync";

const getUserId = () => "dc7a68d3-1072-42b1-8f74-ef062b2257e6";
axios.interceptors.request.use((config) => {
  config.headers['userId'] = getUserId();
  return config;
});

function buildGraphQLQuery(filters) {
  const params = [];
  if (filters.userId) params.push(`userId: "${filters.userId}"`);
  if (filters.dateFrom) params.push(`from: "${filters.dateFrom}T00:00:00Z"`);
  if (filters.dateTo) params.push(`to: "${filters.dateTo}T23:59:59Z"`);
  if (filters.donationGiven !== undefined && filters.donationGiven !== null) params.push(`donationGiven: ${filters.donationGiven}`);

  return `query GetEventParticipationReport {
    eventParticipation(
      ${params.join('\n      ')}
    ) {
      year
      month
      events {
        day
        eventName
        description
        donations {
          donationId
          quantity
        }
      }
    }
  }`;
}

function buildFilterStringForSave(filters) {
  const params = [];
  if (filters.userId) params.push(`userId: "${filters.userId}"`);
  if (filters.dateFrom) params.push(`from: "${filters.dateFrom}T00:00:00Z"`);
  if (filters.dateTo) params.push(`to: "${filters.dateTo}T23:59:59Z"`);
  if (filters.donationGiven !== undefined && filters.donationGiven !== null) params.push(`donationGiven: ${filters.donationGiven}`);

  return `query GetEventParticipationReport {
    eventParticipation(
      ${params.join('\n      ')}
    ) {
      year
      month
      events {
        day
        eventName
        description
        donations {
          donationId
          quantity
        }
      }
    }
  }`;
}

export const eventParticipationReportService = {
  async getReport(filtersOrQuery) {
    const query = typeof filtersOrQuery === "string"
      ? filtersOrQuery
      : buildGraphQLQuery(filtersOrQuery);
    const res = await axios.post(gqlURL, { query });
    return res.data?.data?.eventParticipation || [];
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
      type: "EventParticipation"
    });
  },

  async editFilter({ name, filters }) {
    const filterString = buildFilterStringForSave(filters);
    await axios.post(saveFilterURL, {
      name,
      filter: filterString,
      type: "EventParticipation"
    });
  },

  async deleteFilter(name) {
    await axios.delete(`${deleteFilterURL}?name=${encodeURIComponent(name)}`, {
      headers: {
        userId: getUserId(),
      },
    });
  }
};