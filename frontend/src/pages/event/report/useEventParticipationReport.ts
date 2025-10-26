import { eventParticipationReportService } from "../../../services/eventParticipationReportService";
import { useState, useEffect } from "react";

export const useEventParticipationReport = () => {
  const [filters, setFilters] = useState({});
  const [reportData, setReportData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [savedFilters, setSavedFilters] = useState([]);

  const fetchReport = async (customFiltersOrQuery) => {
    setLoading(true);
    try {
      const data = await eventParticipationReportService.getReport(customFiltersOrQuery ?? filters);
      setReportData(data);
    } catch (e) {
      setError(e.message);
    } finally {
      setLoading(false);
    }
  };

  const fetchSavedFilters = async () => {
    try {
      const allFilters = await eventParticipationReportService.getSavedFilters();
      // Solo mostrar los filtros de tipo "EventParticipation"
      setSavedFilters(allFilters.filter(f => f.type === "EventParticipation"));
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
    await eventParticipationReportService.saveFilter({ name, filters });
    fetchSavedFilters();
  };

  const editFilter = async (filterData) => {
    await eventParticipationReportService.editFilter(filterData);
    fetchSavedFilters();
  };

  const deleteFilter = async name => {
    await eventParticipationReportService.deleteFilter(name);
    fetchSavedFilters();
  };

  return {
    filters, setFilters, reportData, loading, error,
    savedFilters, applySavedFilter, fetchReport,
    saveNewFilter, editFilter, deleteFilter
  };
};