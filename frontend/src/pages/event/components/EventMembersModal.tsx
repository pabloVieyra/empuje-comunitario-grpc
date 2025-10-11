import React, { useEffect, useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Btn, ErrorMsg } from "../../users/styles";
import type { Event } from "@/domain/Event";
import { addUserToEvent, removeUserFromEvent } from "@/services/eventService";
import { Label } from "@/pages/login/styles";
import { getUsers } from "@/services/userService";
import type { User } from "@/domain/User";

const useAuth = () => {
  return {
    userId: "u1",                   // Cambia por el id real del usuario logueado
    role: "COORDINADOR",            // Cambia por "PRESIDENTE", "COORDINADOR" o "VOLUNTARIO"
  };
};

interface Member {
  id: string;
  name: string;
  role: "PRESIDENTE" | "COORDINADOR" | "VOLUNTARIO";
}

interface Props {
  event: Event;
  onClose: () => void;
  onRefresh: () => void; // para refrescar la lista de eventos luego de cambios
}

export const EventMembersModal: React.FC<Props> = ({ event, onClose, onRefresh }) => {
  const { userId, role } = useAuth();
  const [members, setMembers] = useState<Member[]>([]);
  const [participants, setParticipants] = useState<string[]>(event.participants || []);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    setTimeout(async () => {
      const users = await getUsers();
      const fetchedMembers: Member[] = users.map((user: User) => ({
        id: user.id,
        name: user.name,
        role: user.role === "PRESIDENT" ? "PRESIDENTE" : user.role === "COORDINATOR" ? "COORDINADOR" : "VOLUNTARIO"
      }));
      setMembers(fetchedMembers);
      setParticipants(event.participants || []);
      setLoading(false);
    }, 500);
  }, [event]);

  const canEditMember = (memberId: string) => {
    if (role === "PRESIDENTE" || role === "COORDINADOR") return true;
    if (role === "VOLUNTARIO") return memberId === userId;
    return false;
  };

  const handleToggle = async (memberId: string, checked: boolean) => {
    setLoading(true);
    setError(null);
    try {
      if (checked) {
        await addUserToEvent(event.id, memberId, "8d0ae711-6bd8-4c23-98ae-531c95a03e56");
        setParticipants(prev => [...prev, memberId]);
      } else {
        await removeUserFromEvent(event.id, memberId, "8d0ae711-6bd8-4c23-98ae-531c95a03e56");
        setParticipants(prev => prev.filter(id => id !== memberId));
      }
      onRefresh();
    } catch (e: any) {
      setError("Error modificando participantes");
    } finally {
      setLoading(false);
    }
  };

  const scrollStyle: React.CSSProperties = {
    maxHeight: "340px",
    overflowY: "auto",
    paddingRight: "8px",
    marginBottom: "24px"
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle>Participantes del evento</ModalTitle>
        {error && <ErrorMsg>{error}</ErrorMsg>}
        <div style={scrollStyle}>
          {members.map(member => (
            <div key={member.id} style={{marginBottom: 8}}>
              <Label>
                <input
                  type="checkbox"
                  checked={participants.includes(member.id)}
                  disabled={!canEditMember(member.id) || loading}
                  onChange={e => handleToggle(member.id, e.target.checked)}
                />
                {member.name} ({member.role})
              </Label>
            </div>
          ))}
        </div>
        <ModalFooter>
          <Btn type="button" onClick={onClose} style={{background: "#eee", color: "#234"}} disabled={loading}>
            Cerrar
          </Btn>
        </ModalFooter>
      </ModalCard>
    </ModalBg>
  );
};