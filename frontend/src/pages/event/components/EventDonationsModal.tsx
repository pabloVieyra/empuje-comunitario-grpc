import React, { useEffect, useState } from "react";
import {
  Box,
  Flex,
  Text,
  Input,
  VStack,
  Spinner,
} from "@chakra-ui/react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Btn, ErrorMsg } from "../../users/styles";
import type { Event } from "@/domain/Event";
import type { Donation } from "@/domain/Donation";
import { getDonations } from "@/services/donationService";
import { registerDonationToEvent } from "@/services/eventService";

const useAuth = () => ({
  userId: "8d0ae711-6bd8-4c23-98ae-531c95a03e56",
  role: "COORDINADOR",
});

interface Props {
  event: Event;
  onClose: () => void;
  onRefresh: () => void;
}

export const EventDonationsModal: React.FC<Props> = ({
  event,
  onClose,
  onRefresh,
}) => {
  const { userId } = useAuth();
  const [inventory, setInventory] = useState<Donation[]>([]);
  const [selectedDonationId, setSelectedDonationId] = useState<string>("");
  const [quantity, setQuantity] = useState<number | "">("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isPastEvent = (event: Event) => {
    const eventDate = new Date(event.eventDateTime);
    const now = new Date();
    const eventDay = Date.UTC(
      eventDate.getUTCFullYear(),
      eventDate.getUTCMonth(),
      eventDate.getUTCDate()
    );
    const nowDay = Date.UTC(
      now.getUTCFullYear(),
      now.getUTCMonth(),
      now.getUTCDate()
    );
    return eventDay <= nowDay;
  };

  useEffect(() => {
    async function fetchDonations() {
      setLoading(true);
      try {
        const donations = await getDonations();
        setInventory(Array.isArray(donations) ? donations : []);
      } catch (e) {
        setError("Error cargando inventario");
      } finally {
        setLoading(false);
      }
    }
    fetchDonations();
    setSelectedDonationId("");
    setQuantity("");
  }, [event]);

  const selectedDonation =
    inventory.find((d) => d.id === selectedDonationId) || null;

  const handleRegister = async () => {
    setLoading(true);
    setError(null);

    if (!selectedDonationId) {
      setError("Selecciona una donación.");
      setLoading(false);
      return;
    }
    if (!quantity || typeof quantity !== "number" || quantity <= 0) {
      setError("Ingresa una cantidad válida.");
      setLoading(false);
      return;
    }
    if (selectedDonation && quantity > selectedDonation.quantity) {
      setError("La cantidad supera la disponible.");
      setLoading(false);
      return;
    }

    try {
      await registerDonationToEvent(
         event.id,
        selectedDonationId,
        quantity,
        userId
      );
      onRefresh();
      onClose();
    } catch (e) {
      setError("Error registrando donación.");
    } finally {
      setLoading(false);
    }
  };

  const scrollStyle: React.CSSProperties = {
    maxHeight: "340px",
    overflowY: "auto",
    paddingRight: "8px",
    marginBottom: "24px",
  };

  if (!isPastEvent(event)) return null;

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#111" }}>
          Registrar Donación Repartida
        </ModalTitle>
        {error && <ErrorMsg>{error}</ErrorMsg>}
        <Box style={scrollStyle}>
          {loading ? (
            <Flex align="center" justify="center" minH="100px">
              <Spinner size="md" color="blue.500" />
            </Flex>
          ) : inventory.length === 0 ? (
            <Text color="#111">No hay donaciones en inventario.</Text>
          ) : (
            <VStack align="stretch" spacing={4}>
              <Box>
                <Text
                  as="label"
                  color="#111"
                  fontWeight="bold"
                  mb={1}
                  display="block"
                  htmlFor="donation-select"
                >
                  Selecciona la donación:
                </Text>
                <select
                  id="donation-select"
                  style={{
                    width: "100%",
                    padding: "8px",
                    fontSize: "1rem",
                    color: "#111",
                    background: "#fff",
                    border: "1px solid #222",
                    borderRadius: "6px",
                    marginBottom: "8px",
                  }}
                  value={selectedDonationId}
                  onChange={e => {
                    setSelectedDonationId(e.target.value);
                    setQuantity("");
                  }}
                  disabled={loading}
                >
                  <option value="">Selecciona...</option>
                  {inventory.map(item => (
                    <option key={item.id} value={item.id}>
                      {item.description} - {item.category} (Disponible: {item.quantity})
                    </option>
                  ))}
                </select>
              </Box>
              <Box>
                <Text
                  as="label"
                  color="#111"
                  fontWeight="bold"
                  mb={1}
                  display="block"
                  htmlFor="donation-quantity"
                >
                  Cantidad a repartir:
                </Text>
                <Input
                  id="donation-quantity"
                  type="number"
                  min={0}
                  max={selectedDonation?.quantity || 0}
                  value={quantity === "" ? "" : quantity}
                  bg="#fff"
                  color="#111"
                  borderColor="#222"
                  onChange={(e) =>
                    setQuantity(
                      Math.max(
                        0,
                        Math.min(
                          selectedDonation?.quantity || 0,
                          Number(e.target.value)
                        )
                      )
                    )
                  }
                  disabled={loading || !selectedDonationId}
                  width="100px"
                />
                {selectedDonation && (
                  <Text color="#111" fontSize="sm" mt={1}>
                    (Disponible: {selectedDonation.quantity})
                  </Text>
                )}
              </Box>
            </VStack>
          )}
        </Box>
        <ModalFooter>
          <Btn
            type="button"
            onClick={onClose}
            style={{ background: "#eee", color: "#111" }}
            disabled={loading}
          >
            Cancelar
          </Btn>
          <Btn
            type="button"
            onClick={handleRegister}
            style={{ background: "#f9c846", color: "#111" }}
            disabled={loading || !selectedDonationId}
          >
            Registrar Donación
          </Btn>
        </ModalFooter>
      </ModalCard>
    </ModalBg>
  );
};