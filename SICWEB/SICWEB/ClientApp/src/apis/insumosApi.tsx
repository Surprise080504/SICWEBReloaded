import axios from "src/utils/axios";

export const getEstiloInsumos = async (value) => {
  const response = await axios.post<{}>("/api/insumos/getEstiloInsumos", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveEstiloInsumos = async (value) => {
  const response = await axios.post<{}>(
    "/api/insumos/saveEstiloInsumos",
    value
  );
  if (response.status === 200) return response.data;
  else return [];
};

export const getEstiloData = async (id) => {
  const response = await axios.post<{}>("/api/insumos/getEstiloData", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getItems = async () => {
  const response = await axios.get<{}>("/api/insumos/getItems");
  if (response.status === 200) return response.data;
  else return [];
};
