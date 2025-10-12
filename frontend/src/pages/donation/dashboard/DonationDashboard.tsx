import React, { useState, useEffect } from "react";
import { CenterBox, Card, Btn, Page, Table } from "../../users/styles";
import SolicitudDonacionModal from "./modals/SolicitudDonacionModal";
import TransferirDonacionModal from "./modals/TransferirDonacionModal";
import OfrecerDonacionModal from "./modals/OfrecerDonacionModal";
import BajaSolicitudModal from "./modals/BajaSolicitudModal";
import { getAllOfferDonations, getAllRequestDonations } from "@/services/externalDataService";

export const DonationDashboard: React.FC = () => {
  const [externalOffers, setExternalOffers] = useState<any[]>([]);
  const [externalRequests, setExternalRequests] = useState<any[]>([]);
  const [loadingOffers, setLoadingOffers] = useState(true);
  const [loadingRequests, setLoadingRequests] = useState(true);
  const [errorOffers, setErrorOffers] = useState<string | null>(null);
  const [errorRequests, setErrorRequests] = useState<string | null>(null);

  // MODALS
  const [showSolicitud, setShowSolicitud] = useState(false);
  const [showTransferir, setShowTransferir] = useState(false);
  const [showOfrecer, setShowOfrecer] = useState(false);
  const [showBaja, setShowBaja] = useState(false);

  const textBlack = { color: "#222" };

  useEffect(() => {
    async function fetchOffers() {
      setLoadingOffers(true);
      setErrorOffers(null);
      try {
        const offers = await getAllOfferDonations();
        setExternalOffers(Array.isArray(offers) ? offers : []);
      } catch (err) {
        setErrorOffers("No se pudieron cargar las ofertas externas.");
      } finally {
        setLoadingOffers(false);
      }
    }
    async function fetchRequests() {
      setLoadingRequests(true);
      setErrorRequests(null);
      try {
        const requests = await getAllRequestDonations();
        setExternalRequests(Array.isArray(requests) ? requests : []);
      } catch (err) {
        setErrorRequests("No se pudieron cargar las solicitudes externas.");
      } finally {
        setLoadingRequests(false);
      }
    }
    fetchOffers();
    fetchRequests();
  }, []);

  console.log(externalRequests)

  return (
    <Page>
      <CenterBox>
        <Card style={{ width: "100%", maxWidth: "1100px", alignItems: "stretch", gap: "22px" }}>
          <h2 style={{ ...textBlack, fontWeight: 700, fontSize: "2rem", textAlign: "center" }}>Dashboard Donaciones</h2>
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              gap: 24,
              alignItems: "center",
              justifyContent: "center",
              width: "100%",
              margin: "0 auto"
            }}
          >
            <Btn style={{ color: "#fff", width: "80%" }} onClick={() => setShowSolicitud(true)}>Solicitar Donación</Btn>
            <Btn style={{ color: "#fff", width: "80%" }} onClick={() => setShowTransferir(true)}>Transferir Donación</Btn>
            <Btn style={{ color: "#fff", width: "80%" }} onClick={() => setShowOfrecer(true)}>Ofrecer Donación</Btn>
            <Btn style={{ background: "#e94e4e", color: "#fff", width: "80%" }} onClick={() => setShowBaja(true)}>Baja Solicitud</Btn>
          </div>

          <h3 style={{ ...textBlack, marginTop: 28, marginBottom: 6, fontWeight: 600 }}>Solicitudes Donaciones</h3>
          {loadingRequests ? (
            <div style={{ color: "#222", textAlign: "center", margin: "18px 0" }}>Cargando solicitudes...</div>
          ) : errorRequests ? (
            <div style={{ color: "#e94e4e", textAlign: "center", margin: "18px 0" }}>{errorRequests}</div>
          ) : (
            <Table>
              <thead>
                <tr style={textBlack}>
                  <th style={textBlack}>Solicitud ID</th>
                  <th style={textBlack}>Organización</th>
                  <th style={textBlack}>Donaciones</th>
                </tr>
              </thead>
              <tbody>
                {externalRequests.length === 0 ? (
                  <tr>
                    <td colSpan={3} style={{ ...textBlack, textAlign: "center" }}>No hay solicitudes externas</td>
                  </tr>
                ) : (
                  externalRequests.map(request =>
                    (request.donations || []).map((donation: any, idx: number) => (
                      <tr key={request.requestId + idx} style={textBlack}>
                        <td style={textBlack}>{request.requestId}</td>
                        <td style={textBlack}>{request.requesterOrgId}</td>
                        <td style={textBlack}>
                          {donation.category} - {donation.description} ({donation.quantity})
                        </td>
                      </tr>
                    ))
                  )
                )}
              </tbody>
            </Table>
          )}

          <h3 style={{ ...textBlack, marginTop: 28, marginBottom: 6, fontWeight: 600 }}>Todas las ofertas</h3>
          {loadingOffers ? (
            <div style={{ color: "#222", textAlign: "center", margin: "18px 0" }}>Cargando ofertas...</div>
          ) : errorOffers ? (
            <div style={{ color: "#e94e4e", textAlign: "center", margin: "18px 0" }}>{errorOffers}</div>
          ) : (
            <Table>
              <thead>
                <tr style={textBlack}>
                  <th style={textBlack}>Oferta ID</th>
                  <th style={textBlack}>Organización</th>
                  <th style={textBlack}>Categoría</th>
                  <th style={textBlack}>Descripción</th>
                  <th style={textBlack}>Cantidad</th>
                </tr>
              </thead>
              <tbody>
                {externalOffers.length === 0 ? (
                  <tr>
                    <td colSpan={5} style={{ ...textBlack, textAlign: "center" }}>No hay ofertas externas</td>
                  </tr>
                ) : (
                  externalOffers.map(offer =>
                    offer.donations.map((donation: any, idx: number) => (
                      <tr key={offer.offerId + idx} style={textBlack}>
                        <td style={textBlack}>{offer.offerId}</td>
                        <td style={textBlack}>{offer.donationOrganizationId}</td>
                        <td style={textBlack}>{donation.category}</td>
                        <td style={textBlack}>{donation.description}</td>
                        <td style={textBlack}>{donation.quantity}</td>
                      </tr>
                    ))
                  )
                )}
              </tbody>
            </Table>
          )}

          {showSolicitud && <SolicitudDonacionModal onClose={() => setShowSolicitud(false)} />}
          {showTransferir && <TransferirDonacionModal onClose={() => setShowTransferir(false)} requests={externalRequests} />}
          {showOfrecer && <OfrecerDonacionModal onClose={() => setShowOfrecer(false)} />}
          {showBaja && <BajaSolicitudModal onClose={() => setShowBaja(false)} requests={externalRequests} />}
        </Card>
      </CenterBox>
    </Page>
  );
};

export default DonationDashboard;