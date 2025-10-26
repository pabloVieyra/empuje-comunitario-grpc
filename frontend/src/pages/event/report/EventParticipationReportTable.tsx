import React from "react";

const EventParticipationReportTable = ({ data }) => (
  <div>
    {data.length === 0 ? (
      <div>No hay resultados para esos filtros</div>
    ) : (
      data.map((month, idx) => (
        <div key={idx} style={{ marginBottom: 32 }}>
          <h3 style={{color: "#23244b", fontWeight: 700}}>
            {month.year}-{month.month}
          </h3>
          <table style={{width: "100%"}}>
            <thead>
              <tr>
                <th>Día</th>
                <th>Nombre Evento</th>
                <th>Descripción</th>
                <th>Donaciones</th>
              </tr>
            </thead>
            <tbody>
              {month.events.map((event, i) => (
                <tr key={i}>
                  <td>{event.day}</td>
                  <td>{event.eventName}</td>
                  <td>{event.description}</td>
                  <td>
                    {(event.donations || []).map((don, ix) =>
                      <div key={ix}>{don.donationId}: {don.quantity}</div>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ))
    )}
  </div>
);

export default EventParticipationReportTable;