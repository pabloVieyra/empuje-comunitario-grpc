import React, { useState } from "react";
import { Btn, Card, ErrorMsg, UsersPage } from "./../users/styles";
import { DonationTable } from "./components/DonationTable";
import { DonationModal } from "./components/DonationModal";
import { useDonations } from "./useDonations";

export const DonationList: React.FC = () => {
  const { donations, loading, error, addDonation, editDonation, removeDonation, setError } = useDonations();
  const [modalOpen, setModalOpen] = useState(false);
  const [editDonationData, setEditDonationData] = useState<any>(null);

  const openNewModal = () => {
    setEditDonationData(null);
    setModalOpen(true);
    setError(null);
  };

  const openEditModal = (donation: any) => {
    setEditDonationData(donation);
    setModalOpen(true);
    setError(null);
  };

  const handleSave = async (id: string | null, data: any) => {
    if (id) {
      await editDonation(id, data);
    } else {
      await addDonation(data);
    }
    setModalOpen(false);
  };

  console.log("Donations:", donations);

  return (
    <UsersPage>
      <Card>
        <div style={{display: "flex", justifyContent: "space-between", alignItems: "center", flexDirection: "column", marginBottom: 18}}>
          <h2 style={{
            fontWeight: 700,
            fontSize: "2.2rem",
            color: "#23244b",
            letterSpacing: "0.01em",
          }}>
            ABM de Donaciones
          </h2>
          <Btn onClick={openNewModal}>+ Nueva Donación</Btn>
        </div>
        {error && <ErrorMsg>{error}</ErrorMsg>}
        {loading && <div style={{margin:'18px 0'}}>Cargando...</div>}
        <DonationTable
          donations={donations}
          onEdit={openEditModal}
          onDelete={removeDonation}
        />
      </Card>
      {modalOpen && (
        <DonationModal
          donation={editDonationData}
          onClose={() => setModalOpen(false)}
          onSave={handleSave}
          error={error}
        />
      )}
    </UsersPage>
  );
};