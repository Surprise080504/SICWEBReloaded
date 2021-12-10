import axios from "src/utils/axios";

export const getOrdenEstados = async () => {
  const response = await axios.get<{}>("/api/compras/getOrdenEstados");
  if (response.status === 200) return response.data;
  else return [];
};

export const getOrdens = async (filter) => {
  const response = await axios.post<{}>("/api/compras/getOrdens", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const getOrdenDetail = async (id) => {
  const response = await axios.post<{}>("/api/compras/getOrdenDetail", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getOrdenItems = async (id) => {
  const response = await axios.post<{}>("/api/compras/getOrdenItems", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const deleteOrdens = async (id) => {
  const response = await axios.post<{}>("/api/compras/deleteOrdens", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const saveOrdens = async (value) => {
  const response = await axios.post<{}>("/api/compras/saveOrdens", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getClase = async () => {
  const response = await axios.get<{}>("/api/compras/getClase");
  if (response.status === 200) return response.data;
  else return [];
};

export const obtNroSerie = async () => {
  const response = await axios.get<{}>("/api/compras/obtNroSerie");
  if (response.status === 200) return response.data;
  else return [];
};

export const getMoneda = async () => {
  const response = await axios.get<{}>("/api/compras/getMoneda");
  if (response.status === 200) return response.data;
  else return [];
};

export const getDirection = async () => {
  const response = await axios.get<{}>("/api/compras/getDirection");
  if (response.status === 200) return response.data;
  else return [];
};

export const getDlvrAddr = async () => {
  const response = await axios.get<{}>("/api/compras/getDlvrAddr");
  if (response.status === 200) return response.data;
  else return [];
};

export const getEstado = async () => {
  const response = await axios.get<{}>("/api/compras/getEstado");
  if (response.status === 200) return response.data;
  else return [];
};

export const download = async () => {
  axios({
    url: "/api/compras/download",
    method: "GET",
    responseType: "blob", // important
  }).then((response) => {
    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", "file.xlsx");
    document.body.appendChild(link);
    link.click();
  });
};
