import axios from 'src/utils/axios';

//export const getCompany = async (empresa) => {
//    const response = await axios.get<{}>('/api/empresa/', {
//        params: {
//            id: empresa
//        } });
//    if (response.status === 200) return response.data;
//    else return [];
//}


export const getCentrosCosto = async () => {
    const response = await axios.get<{}>('/api/empresa/allcentroscosto', {});
    if (response.status === 200) return response.data;
    else return [];
}

export const getAddresses = async () => {
    const response = await axios.get<{}>('/api/empresa/alladdresses', {});
    if (response.status === 200) return response.data;
    else return [];
}

