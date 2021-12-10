import axios from "src/utils/axios";

export const getEstados = async () => {
  const response = await axios.get<{}>("/api/entrada/getEstados");
  if (response.status === 200) return response.data;
  else return [];
};

export const getEntradas = async (filter) => {
  const response = await axios.post<{}>("/api/entrada/getEntradas", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const getOrdens = async (filter) => {
  const response = await axios.post<{}>("/api/entrada/getOrdens", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const getItems = async (id) => {
  const response = await axios.post<{}>("/api/entrada/getItems", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getAlmacens = async (filter) => {
  const response = await axios.post<{}>("/api/entrada/getAlmacens", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveEntrada = async (data) => {
  const response = await axios.post<{}>("/api/entrada/saveEntrada", data);
  if (response.status === 200) return response.data;
  else return [];
};

export const getItemsById = async (id) => {
  const response = await axios.post<{}>("/api/entrada/getItemsById", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getAlmacenById = async (id) => {
  const response = await axios.post<{}>("/api/entrada/getAlmacenById", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getOrdenById = async (id) => {
  const response = await axios.post<{}>("/api/entrada/getOrdenById", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getDetailEntradaById = async (id) => {
  const response = await axios.post<{}>("/api/entrada/getDetailEntradaById", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const changeToCerrar = async (id) => {
  const response = await axios.post<{}>("/api/entrada/changeToCerrar", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const changeToAnular = async (id) => {
  const response = await axios.post<{}>("/api/entrada/changeToAnular", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};
