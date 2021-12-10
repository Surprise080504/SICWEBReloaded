import axios from "src/utils/axios";

export const getBrands = async () => {
  const response = await axios.get<{}>("/api/style/allBrands", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getColor = async (value) => {
  const response = await axios.post<{}>("/api/style/getColor", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getCategory = async (value) => {
  const response = await axios.post<{}>("/api/style/getCategory", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getOriginCategories = async () => {
  const response = await axios.get<{}>("/api/style/getOriginCategory");
  if (response.status === 200) return response.data;
  else return [];
};

export const getOriginColors = async () => {
  const response = await axios.get<{}>("/api/style/allOriginColors", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getColors = async () => {
  const response = await axios.get<{}>("/api/style/allColors", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const saveColor = async (value) => {
  const response = await axios.post<{}>("/api/style/saveColor", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveCategoryDetail = async (value) => {
  const response = await axios.post<{}>("/api/style/saveCategoryDetail", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getCategories = async () => {
  const response = await axios.get<{}>("/api/style/allCategories", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const saveCategory = async (value) => {
  const response = await axios.post<{}>("/api/style/saveCategory", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveMarca = async (value) => {
  const response = await axios.post<{}>("/api/style/saveMarca", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getTallas = async () => {
  const response = await axios.get<{}>("/api/style/allTallas", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getCurTallas = async (id) => {
  const response = await axios.post<{}>("/api/style/getCurTallas", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};
export const saveStyle = async (value) => {
  // const _res = await axios.post<{}>('/api/style/imageUpload', formData);

  const _res = await axios.post<{}>("/api/style/saveStyle", value);
  if (_res.status === 200) {
  } else return _res.data;

  if (value.imageChange === true) {
    const formData = new FormData();
    formData.append("image", value.image);
    formData.append("_id", _res.data.toString());

    const response = await axios.post<{}>("/api/style/imageUpload", formData);

    if (response.status === 200) return _res.data;
    //return response.data;
    else return _res.data;
  } else {
    return _res.data;
  }
};
export const getStyle = async (value) => {
  const response = await axios.post<{}>("/api/style/getStyle2", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getEditAccess = async (id) => {
  const response = await axios.post<{}>("/api/style/getEditAccess", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const _getStyle = async (value) => {
  const response = await axios.post<{}>("/api/style/getStyle", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getCurStyle = async (value) => {
  const response = await axios.post<{}>("/api/style/getStyle", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const getEstiloTallaID = async (value) => {
  const response = await axios.post<{}>("/api/style/getEstiloTallaID", value);
  if (response.status === 200) return response.data;
  else return [];
};

export const deleteStyle = async (id) => {
  const response = await axios.post<{}>("/api/style/deleteStyle", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getImage = async (id) => {
  const response = await axios.post<{}>("/api/file/getImage", { id: id });
  if (response.status === 200) return response.data;
  else return null;
};

export const getEstiloCombList = async (client) => {
  const response = await axios.post<{}>("/api/style/getEstiloCombList", {
    client: client,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getSizeList = async (id) => {
  const response = await axios.post<{}>("/api/style/getSizeList", { id: id });
  if (response.status === 200) return response.data;
  else return null;
};
