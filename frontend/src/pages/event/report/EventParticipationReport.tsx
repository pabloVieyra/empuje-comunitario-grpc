import React, { useState } from "react";
import { useEventParticipationReport } from "./useEventParticipationReport";
import SavedEventFiltersList from "./SavedEventFiltersList";
import EventParticipationReportFilters from "./EventParticipationReportFilters";
import SaveEventFilterModal from "./SaveEventFilterModal";
import EventParticipationReportTable from "./EventParticipationReportTable";
import EditEventFilterModal from "./EditEventFilterModal";


const EventParticipationReport: React.FC = () => {
  const {
    reportData, filters, setFilters, loading, error,
    savedFilters, applySavedFilter, fetchReport,
    saveNewFilter, editFilter, deleteFilter
  } = useEventParticipationReport();

  const [saveModalOpen, setSaveModalOpen] = useState(false);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [filterToEdit, setFilterToEdit] = useState(null);

  return (
    <div>
      <h2>Informe de Participación en Eventos</h2>
      <EventParticipationReportFilters filters={filters} setFilters={setFilters} onSearch={fetchReport} />
      <button onClick={() => setSaveModalOpen(true)}>Guardar Filtro</button>
      <SavedEventFiltersList
        filters={savedFilters}
        onApply={applySavedFilter}
        onEdit={f => { setFilterToEdit(f); setEditModalOpen(true); }}
        onDelete={deleteFilter}
      />
      {loading ? <div>Cargando...</div> : <EventParticipationReportTable data={reportData} />}
      {error && <div style={{color: "red"}}>{error}</div>}
      {saveModalOpen &&
        <SaveEventFilterModal
          filters={filters}
          onClose={() => setSaveModalOpen(false)}
          onSave={saveNewFilter}
        />
      }
      {editModalOpen && filterToEdit &&
        <EditEventFilterModal
          filter={filterToEdit}
          onClose={() => setEditModalOpen(false)}
          onEdit={editFilter}
        />
      }
    </div>
  );
};
export default EventParticipationReport;