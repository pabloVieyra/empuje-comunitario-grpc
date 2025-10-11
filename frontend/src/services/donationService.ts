import type { Donation } from "@/domain/Donation";
import { apiClient } from "./apiClient";

export async function getDonations(): Promise<Donation[]> {
  const { data } = await apiClient.get("/Donation/ListDonationInventoryAsync");
  return data?.data?.inventories || [];
}

export async function createDonation(donation: Omit<Donation, "id">): Promise<Donation> {
  const { data } = await apiClient.post("/Donation/CreateDonationInventoryAsync", donation);
  return data.data || {};
}

export async function updateDonation(id: string, donation: Partial<Omit<Donation, "id">>): Promise<Donation> {
  const { data } = await apiClient.patch(`/Donation/UpdateDonationInventoryAsync`, { id, ...donation });
  return data.data || {};
}

export async function deleteDonation(id: string): Promise<void> {
  await apiClient.delete(`/Donation/DeleteDonationInventoryAsync`, { data: { id } });
}