import React, { useState } from "react";
import { useUsers } from "./useUsers";
import { Btn, Card, ErrorMsg, UsersPage } from "./styles";
import { UserTable } from "./components/UserTable";
import { UserModal } from "./components/UserModal";


export const UserList: React.FC = () => {
  const { users, loading, error, addUser, editUser, deactivate, setError } = useUsers();
  const [modalOpen, setModalOpen] = useState(false);
  const [editUserData, setEditUserData] = useState<any>(null);

  const openNewModal = () => {
    setEditUserData(null);
    setModalOpen(true);
    setError(null);
  };

  const openEditModal = (user: any) => {
    setEditUserData(user);
    setModalOpen(true);
    setError(null);
  };

  const handleSave = async (id: string | null, data: any) => {
    if (id) {
      await editUser(id, data);
    } else {
      await addUser(data);
    }
    setModalOpen(false);
  };

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
            ABM de Usuarios
          </h2>
          <Btn onClick={openNewModal}>+ Nuevo Usuario</Btn>
        </div>
        {error && <ErrorMsg>{error}</ErrorMsg>}
        {loading && <div style={{margin:'18px 0'}}>Cargando...</div>}
        <UserTable
          users={users}
          onEdit={openEditModal}
          onDeactivate={deactivate}
        />
      </Card>
      {modalOpen && (
        <UserModal
          user={editUserData}
          onClose={() => setModalOpen(false)}
          onSave={handleSave}
          error={error}
        />
      )}
    </UsersPage>
  );
};