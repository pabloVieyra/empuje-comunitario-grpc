import React from "react";

const EventParticipationReportFilters = ({ filters = {}, setFilters, onSearch }) => (
  <div>
    <label>
      Usuario:
      <input
        type="text"
        value={filters.userId || ""}
        onChange={e => setFilters({ ...filters, userId: e.target.value })}
        required
      />
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
      Reparto de Donaciones:
      <select
        value={filters.donationGiven ?? ""}
        onChange={e =>
          setFilters({ ...filters, donationGiven: e.target.value === "" ? null : e.target.value === "true" })
        }
      >
        <option value="">Ambos</option>
        <option value="false">No</option>
        <option value="true">Sí</option>
      </select>
    </label>
    <button onClick={() => onSearch()}>Buscar</button>
  </div>
);

export default EventParticipationReportFilters;