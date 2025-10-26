import axios from "axios";

const getUserId = () => "dc7a68d3-1072-42b1-8f74-ef062b2257e6";
const baseURL = "http://localhost:5082";

export async function getAllPresidents(orgIds: string[]) {
  const { data } = await axios.post(
    `${baseURL}/Organization/GetAllPresident`,
    orgIds,
    { headers: { UserId: getUserId() } }
  );
  return (
    data?.data?.list_presidentsResponse?.list_presidentsResult ??
    []
  );
}

export async function getAllOrganizations(orgIds: string[]) {
  const { data } = await axios.post(
    `${baseURL}/Organization/GetAllOrganization`,
    orgIds,
    { headers: { UserId: getUserId() } }
  );
  return (
    data?.data?.list_associationsResponse?.list_associationsResult ??
    []
  );
}