import { useState, useEffect } from "react";
import { getDonations, createDonation, updateDonation, deleteDonation } from "@/services/donationService";
import type { Donation } from "@/domain/Donation";

export const useDonations = () => {
  const [donations, setDonations] = useState<Donation[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchDonations = async () => {
    setLoading(true);
    try {
        console.log("Fetching donations...");
        const donations = await getDonations();
        console.log("Donations fetched:", donations);
      setDonations(donations);
    } catch (e: any) {
      setError(e.message || "Error cargando donaciones");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchDonations(); }, []);

  const addDonation = async (donationData: Omit<Donation, "id">) => {
    setLoading(true);
    try {
      const newDonation = await createDonation(donationData);
      setDonations([...donations, newDonation]);
    } catch (e: any) {
      setError(e.message || "Error creando donación");
    } finally {
      setLoading(false);
    }
  };

  const editDonation = async (id: string, data: Partial<Omit<Donation, "id">>) => {
    setLoading(true);
    try {
      const updated = await updateDonation(id, data);
      setDonations(donations.map(d => d.id === id ? updated : d));
    } catch (e: any) {
      setError(e.message || "Error modificando donación");
    } finally {
      setLoading(false);
    }
  };

  const removeDonation = async (id: string) => {
    setLoading(true);
    try {
      await deleteDonation(id);
      setDonations(donations.filter(d => d.id !== id));
    } catch (e: any) {
      setError(e.message || "Error eliminando donación");
    } finally {
      setLoading(false);
    }
  };

  return { donations, loading, error, addDonation, editDonation, removeDonation, fetchDonations, setError };
};