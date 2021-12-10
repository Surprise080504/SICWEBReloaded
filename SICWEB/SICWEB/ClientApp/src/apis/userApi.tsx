import { MenuItem } from "@material-ui/core";
import { profile } from "console";
import { userInfo } from "os";
import axios, { userFormeAxiosInstance } from "src/utils/axios";
import { formatDiagnosticsWithColorAndContext } from "typescript";
import { saveOPC } from "./menuApi";

export const getUser = async (filter) => {
  const response = await axios.post<{}>("/api/user/users", filter);
  if (response.status === 200) return response.data;
  else return [];
};

export const deleteUser = async (id) => {
  const response = await axios.post<{}>("/api/user/deleteUser", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const saveUser = async (saveUser, userImage = "") => {
  let formData = new FormData();
  formData.append("userImage", userImage);
  for (var key in saveUser) {
    formData.append(key, saveUser[key]);
  }

  const response = await userFormeAxiosInstance.post<{}>(
    "/api/user/saveUser",
    formData
  );
  if (response.status === 200) return response.data;
  else return [];
};

export const getProfiles = async () => {
  const response = await axios.get<{}>("/api/user/getProfiles");
  if (response.status === 200) return response.data;
  else return [];
};

export const saveProfile = async (saveProfile) => {
  const response = await axios.post<{}>("/api/user/saveProfile", saveProfile);
  if (response.status === 200) return response.data;
  else return [];
};

export const getProfile = async (id) => {
  const response = await axios.post<{}>("/api/user/getProfile", { id: id });
  if (response.status === 200) return response.data;
  else return [];
};

export const getAccessProfile = async (profileid) => {
  const response = await axios.post<{}>(
    "/api/user/getAccessProfile",
    profileid
  );
  if (response.status === 200) return response.data;
  else return [];
};

export const getCheckedValues = async (profileid) => {
  const response = await axios.post<{}>("/api/user/getCheckedValues", {
    id: profileid,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getCheckedcrudValues = async (profileid) => {
  const response = await axios.post<{}>("/api/user/getCheckedcrudValues", {
    id: profileid,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const checkImageFile = async (filename) => {
  const response = await axios.post<{}>("/api/user/checkImageFile", {
    id: filename,
  });
  if (response.status === 200) return response.data;
  else return [];
};

export const getPermission = (menuPermission = [], pathname = "") => {
  if (menuPermission != null) {
    return (
      menuPermission
        .reduce((newArr, cur) => {
          return [...newArr, ...(cur?.children ?? [])];
        }, [])
        .filter((item) =>
          `/${item?.menu_c_vpag_asp.toUpperCase()}`.includes(
            pathname.toUpperCase()
          )
        )?.[0] ?? {}
    );
  } else {
    return {};
  }
};
