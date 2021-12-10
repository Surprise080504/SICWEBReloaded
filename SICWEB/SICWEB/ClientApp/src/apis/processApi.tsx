import axios from "src/utils/axios";

export const getProcess = async () => {
  const response = await axios.get<{}>("/api/process/allProcess", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getEstiloProcess = async (value) => {
  const response = await axios.post<{}>("/api/process/getEstiloProcess", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveEstiloProcess = async (value) => {
  const response = await axios.post<{}>(
    "/api/process/saveEstiloProcess",
    value
  );
  if (response.status === 200) return response.data;
  else return [];
};

export const saveProcess = async (value) => {
  const response = await axios.post<{}>("/api/process/saveProcess", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveChildItem = async (value) => {
  const response = await axios.post<{}>("/api/process/saveChildItem", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getCheckedTallas = async (id) => {
  const response = await axios.post<{}>("/api/process/getCheckedTallas", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getEstiloData = async (id) => {
  const response = await axios.post<{}>("/api/process/getEstiloData", {
    id: id,
  });
  if (response.status === 200) return response.data;
  else return [];
};
