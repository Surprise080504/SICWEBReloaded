import axios from "src/utils/axios";
export const getClientes = async (filter) => {
  const response = await axios.post<{}>("/api/cliente/clientes", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveCliente = async (saveItem) => {
  const response = await axios.post<{}>("/api/cliente/saveCliente", saveItem);
  if (response.status === 200) return response.data;
  else return [];
};

export const deleteCliente = async (id) => {
  const response = await axios.post<{}>("/api/cliente/deleteCliente", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getClients = async (value) => {
  const response = await axios.post<{}>("/api/client/clients", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveClient = async (value) => {
  const response = await axios.post<{}>("/api/client/saveClient", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getCargo = async () => {
  const response = await axios.get<{}>("/api/client/allCargo", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getDepartment = async () => {
  const response = await axios.get<{}>("/api/client/allDepartment", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getProvince = async (d) => {
  const response = await axios.post<{}>("/api/client/allProvince", { id: d });
  if (response.status === 200) return response.data;
  else return [];
};

export const getDistrict = async (d) => {
  const response = await axios.post<{}>("/api/client/allDistrict", { id: d });
  if (response.status === 200) return response.data;
  else return [];
};

export const saveContact = async (vals) => {
  const response = await axios.post<{}>("/api/client/saveContact", vals);
  if (response.status === 200) return response.data;
  else return [];
};

export const formatContact = async () => {
  const response = await axios.post<{}>("/api/client/formatContact");
  if (response.status === 200) return response.data;
  else return [];
};
