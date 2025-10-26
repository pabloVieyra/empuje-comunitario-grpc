import { donationReportService } from "@/services/donationReportService";
import { useState, useEffect } from "react";

export const useDonationReport = () => {
  const [filters, setFilters] = useState({});
  const [reportData, setReportData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [excelLoading, setExcelLoading] = useState(false);
  const [error, setError] = useState(null);
  const [savedFilters, setSavedFilters] = useState([]);

  const fetchReport = async (customFiltersOrQuery) => {
    setLoading(true);
    try {
      const data = await donationReportService.getReport(customFiltersOrQuery ?? filters);
      setReportData(data);
    } catch (e) {
      setError(e.message);
    } finally {
      setLoading(false);
    }
  };

  const fetchSavedFilters = async () => {
    try {
      const allFilters = await donationReportService.getSavedFilters();
      setSavedFilters(allFilters.filter(f => f.type === "Donation"));
    } catch (e) {
      setSavedFilters([]);
    }
  };

  useEffect(() => { fetchSavedFilters(); }, []);

  const applySavedFilter = f => {
    try {
      const maybeObj = JSON.parse(f.filter);
      setFilters(maybeObj);
      fetchReport(maybeObj);
    } catch {
      fetchReport(f.filter);
    }
  };

  const saveNewFilter = async (name, filters) => {
    await donationReportService.saveFilter({ name, filters });
    fetchSavedFilters();
  };

  const editFilter = async (filterData) => {
    await donationReportService.editFilter(filterData);
    fetchSavedFilters();
  };

  const deleteFilter = async (name) => {
    await donationReportService.deleteFilter(name);
    fetchSavedFilters();
  };

  const downloadExcel = async () => {
    setExcelLoading(true);
    await donationReportService.downloadExcel(filters);
    setExcelLoading(false);
  };

  return {
    filters, setFilters, reportData, loading, error,
    savedFilters, applySavedFilter, fetchReport,
    saveNewFilter, editFilter, deleteFilter,
    excelLoading, downloadExcel
  };
};