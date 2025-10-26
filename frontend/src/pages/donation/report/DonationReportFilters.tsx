import React from "react";

const DonationReportFilters = ({ filters = {}, setFilters, onSearch }) => (
  <div>
    <label>
      Categoría:
      <select value={filters.category || ""} onChange={e => setFilters({ ...filters, category: e.target.value })}>
        <option value="">Todas</option>
        <option value="ROPA">Ropa</option>
        <option value="ALIMENTO">Alimento</option>
        {/* ... otras categorías */}
      </select>
    </label>
    <label>
      Fecha desde:
      <input type="date" value={filters.dateFrom || ""} onChange={e => setFilters({ ...filters, dateFrom: e.target.value })} />
    </label>
    <label>
      Fecha hasta:
      <input type="date" value={filters.dateTo || ""} onChange={e => setFilters({ ...filters, dateTo: e.target.value })} />
    </label>
    <label>
      Eliminado:
      <select value={filters.eliminated ?? ""} onChange={e => setFilters({ ...filters, eliminated: e.target.value === "" ? null : e.target.value === "true" })}>
        <option value="">Ambos</option>
        <option value="false">No</option>
        <option value="true">Sí</option>
      </select>
    </label>
    <button onClick={() => onSearch()}>Buscar</button>
  </div>
);

export default DonationReportFilters;