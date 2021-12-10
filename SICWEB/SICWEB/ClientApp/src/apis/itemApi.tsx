import axios from "src/utils/axios";

export const getSegments = async () => {
  const response = await axios.get<{}>("/api/item/segments", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getProducts = async () => {
  const response = await axios.get<{}>("/api/item/products", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getProduct = async (id) => {
  const response = await axios.post<{}>("/api/item/product", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getFamilies = async () => {
  const response = await axios.get<{}>("/api/item/allfamilies", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const getFamilies1 = async (segment) => {
  const response = await axios.post<{}>("/api/item/families", { id: segment });
  if (response.status === 200) return response.data;
  else return [];
};

export const getSubFamilies = async (family) => {
  const response = await axios.post<{}>("/api/item/subfamilies", {
    id: family,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getFamilyAndSub = async (pid) => {
  const response = await axios.post<{}>("/api/item/familysub", { id: pid });
  if (response.status === 200) return response.data;
  else return [];
};

export const getPermissionOption = async (user) => {
  const response = await axios.post<{}>("/api/item/getPermissionOption", {
    id: user,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getUnits = async () => {
  const response = await axios.get<{}>("/api/item/units", {});
  if (response.status === 200) return response.data;
  else return [];
};

export const saveFamily = async (family) => {
  const response = await axios.post<{}>("/api/item/saveFamily", family);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveSubFamily = async (subFamily) => {
  const response = await axios.post<{}>("/api/item/SaveSubFamily", subFamily);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveUnit = async (unit) => {
  const response = await axios.post<{}>("/api/item/saveunit", unit);
  if (response.status === 200) return response.data;
  else return [];
};

export const saveItem = async (saveItem) => {
  const _response = await axios.post<{}>("/api/item/saveitem", saveItem);
  if (_response.status === 200) {
  } else return [];

  if (saveItem.imageChange === true) {
    const formData = new FormData();
    formData.append("image", saveItem.image);
    formData.append("_id", _response.data.toString());

    const __response = await axios.post<{}>("/api/item/imageUpload", formData);

    if (__response.status === 200) return __response.data;
    //return response.data;
    else return __response.data;
  } else {
    return [];
  }
};

export const deleteItem = async (id) => {
  const response = await axios.post<{}>("/api/item/deleteItem", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getItem = async (filter) => {
  const response = await axios.post<{}>("/api/item/items", filter);
  if (response.status === 200) return response.data;
  else return [];
};
export const getItemChecked = async (filter) => {
  const response = await axios.post<{}>("/api/item/getitems", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const getProveedorChecked = async (filter) => {
  const response = await axios.post<{}>(
    "/api/item/getProveedorChecked",
    filter
  );
  if (response.status === 200) return response.data;
  else return [];
};

export const checkImageFile = async (filename) => {
  const response = await axios.post<{}>("/api/item/checkImageFile", {
    id: filename,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const checkItemImageFile = async (filename) => {
  const response = await axios.post<{}>("/api/item/checkItemImageFile", {
    id: filename,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const download = async () => {
  axios({
    url: "/api/item/download",
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
