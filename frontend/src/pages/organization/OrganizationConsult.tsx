import React, { useState } from "react";
import { Card, Btn, CenterBox, Page, Table, ErrorMsg, Input } from "../users/styles";
import { Box, Text } from "@chakra-ui/react";
import { getAllPresidents, getAllOrganizations } from "@/services/organizationService";

const OrganizationConsult: React.FC = () => {
  const [orgIds, setOrgIds] = useState("");
  const [presidents, setPresidents] = useState([]);
  const [organizations, setOrganizations] = useState([]);
  const [loadingPres, setLoadingPres] = useState(false);
  const [loadingOrg, setLoadingOrg] = useState(false);
  const [errorPres, setErrorPres] = useState<string | null>(null);
  const [errorOrg, setErrorOrg] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => setOrgIds(e.target.value);

  // Helper para parsear IDs ingresados
  const parseIds = (txt: string) =>
    txt.split(/[,\s]+/).map(x => x.trim()).filter(Boolean);

  const handlePresidents = async () => {
    setLoadingPres(true);
    setErrorPres(null);
    try {
      const ids = parseIds(orgIds);
      const res = await getAllPresidents(ids);
      setPresidents(res);
    } catch (e) {
      setErrorPres("No se pudo obtener presidentes de esas ONGs.");
    } finally {
      setLoadingPres(false);
    }
  };

  const handleOrganizations = async () => {
    setLoadingOrg(true);
    setErrorOrg(null);
    try {
      const ids = parseIds(orgIds);
      const res = await getAllOrganizations(ids);
      setOrganizations(res);
    } catch (e) {
      setErrorOrg("No se pudo obtener datos de esas ONGs.");
    } finally {
      setLoadingOrg(false);
    }
  };

  return (
    <Page>
      <CenterBox>
        <Card style={{ width: "100%", maxWidth: "900px", alignItems: "stretch", gap: "32px" }}>
          <Text fontWeight={800} fontSize="2.2rem" color="#23244b" textAlign="center" mb={4}>
            Consulta de Presidentes y ONGs
          </Text>
          <Box mb={2}>
            <Text fontWeight={600} color="#23244b" mb={1}>
              IDs de organizaciones (separados por coma, espacio o enter):
            </Text>
            <Input
              value={orgIds}
              onChange={handleChange}
              placeholder='Ej: 1, 2, 3'
              style={{ background: "#f9fbff", color: "#23244b" }}
            />
            <Box display="flex" gap={12} mt={2}>
              <Btn style={{ background: "#2456e9" }} onClick={handlePresidents} disabled={loadingPres}>
                Consultar Presidentes
              </Btn>
              <Btn style={{ background: "#19c27c" }} onClick={handleOrganizations} disabled={loadingOrg}>
                Consultar ONGs
              </Btn>
            </Box>
          </Box>

          {/* Presidents Table */}
          <Box mt={4}>
            <Text fontWeight={700} fontSize="1.2rem" color="#23244b" mb={2}>Presidentes</Text>
            {errorPres && <ErrorMsg>{errorPres}</ErrorMsg>}
            {loadingPres ? (
              <Text>Cargando presidentes...</Text>
            ) : presidents.length > 0 ? (
              <Table>
                <thead>
                  <tr style={{ color: "#000" }}>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Dirección</th>
                    <th>Teléfono</th>
                    <th>Org ID</th>
                  </tr>
                </thead>
                <tbody>
                  {presidents.map((p, idx) => (
                    <tr key={idx} style={{ color: "#000" }}>
                      <td style={{ color: "#000" }}>{p.id}</td>
                      <td style={{ color: "#000" }}>{p.name}</td>
                      <td style={{ color: "#000" }}>{p.address}</td>
                      <td style={{ color: "#000" }}>{p.phone}</td>
                      <td style={{ color: "#000" }}>{p.organization_id}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            ) : (
              <Text color="#666" fontSize="1rem">Sin datos</Text>
            )}
          </Box>

          {/* Organizations Table */}
          <Box mt={4}>
            <Text fontWeight={700} fontSize="1.2rem" color="#23244b" mb={2}>ONGs</Text>
            {errorOrg && <ErrorMsg>{errorOrg}</ErrorMsg>}
            {loadingOrg ? (
              <Text>Cargando ONGs...</Text>
            ) : organizations.length > 0 ? (
              <Table>
                <thead>
                  <tr style={{ color: "#000" }}>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Dirección</th>
                    <th>Email</th>
                  </tr>
                </thead>
                <tbody>
                  {organizations.map((o, idx) => (
                    <tr key={idx} style={{ color: "#000" }}>
                      <td style={{ color: "#000" }}>{o.id}</td>
                      <td style={{ color: "#000" }}>{o.name}</td>
                      <td style={{ color: "#000" }}>{o.address ?? "-"}</td>
                      <td style={{ color: "#000" }}>{o.email ?? "-"}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            ) : (
              <Text color="#666" fontSize="1rem">Sin datos</Text>
            )}
          </Box>
        </Card>
      </CenterBox>
    </Page>
  );
};
export default OrganizationConsult;