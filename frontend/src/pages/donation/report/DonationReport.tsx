import React, { useState } from "react";
import { useDonationReport } from "./useDonationReport";
import DonationReportFilters from "./DonationReportFilters";
import SavedFiltersList from "./SavedFiltersList";
import DonationReportTable from "./DonationReportTable";
import SaveFilterModal from "./SaveFilterModal";
import EditFilterModal from "./EditFilterModal";

const DonationReport: React.FC = () => {
  const {
    reportData, filters, setFilters, loading, error,
    savedFilters, applySavedFilter, fetchReport,
    saveNewFilter, editFilter, deleteFilter,
    excelLoading, downloadExcel
  } = useDonationReport();

  const [saveModalOpen, setSaveModalOpen] = useState(false);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [filterToEdit, setFilterToEdit] = useState(null);

  return (
    <div>
      <h2>Informe de Donaciones</h2>
      <DonationReportFilters filters={filters} setFilters={setFilters} onSearch={fetchReport} />
      <button onClick={() => setSaveModalOpen(true)}>Guardar Filtro</button>
      <SavedFiltersList
        filters={savedFilters}
        onApply={applySavedFilter}
        onEdit={f => { setFilterToEdit(f); setEditModalOpen(true); }}
        onDelete={deleteFilter}
      />
      <button onClick={downloadExcel} disabled={excelLoading}>Descargar Excel</button>
      {loading ? <div>Cargando...</div> : <DonationReportTable data={reportData} />}
      {error && <div style={{color: "red"}}>{error}</div>}
      {saveModalOpen &&
        <SaveFilterModal
          filters={filters}
          onClose={() => setSaveModalOpen(false)}
          onSave={saveNewFilter}
        />
      }
      {editModalOpen && filterToEdit &&
        <EditFilterModal
          filter={filterToEdit}
          onClose={() => setEditModalOpen(false)}
          onEdit={editFilter}
        />
      }
    </div>
  );
};

export default DonationReport;