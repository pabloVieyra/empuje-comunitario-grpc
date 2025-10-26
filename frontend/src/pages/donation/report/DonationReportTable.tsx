import React from "react";

const DonationReportTable = ({ data }) => (
  <table>
    <thead>
      <tr>
        <th>Categoría</th>
        <th>Eliminado</th>
        <th>Total Cantidad</th>
      </tr>
    </thead>
    <tbody>
      {data.map((row, idx) => (
        <tr key={idx}>
          <td>{row.category}</td>
          <td>{row.eliminated ? "Sí" : "No"}</td>
          <td>{row.totalQuantity}</td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default DonationReportTable;