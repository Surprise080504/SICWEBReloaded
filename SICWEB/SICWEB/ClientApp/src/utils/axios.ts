import axios from 'axios';

const axiosInstance = axios.create();
// axiosInstance.defaults.baseURL = 'http://localhost:830' 
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => Promise.reject((error.response && error.response.data) || 'Something went wrong')
);

export const userFormeAxiosInstance = axios.create({headers: {'Content-Type': 'multipart/form-data' }})
userFormeAxiosInstance.interceptors.response.use(
  (response) => response,
  (error) => Promise.reject((error.response && error.response.data) || 'Something went wrong')
);


export default axiosInstance;
