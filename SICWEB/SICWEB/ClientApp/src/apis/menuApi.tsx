import axios from 'src/utils/axios';

export const getMenuList = async (userid) => {
    const response = await axios.post<{}>('/api/data/menu', {id:userid});
    if (response.status === 200) return response.data;
    else return [];
}


export const deleteMenu = async (id) => {
    const response = await axios.post<{}>('/api/menu/deleteMenu', { id: id });
    if (response.status === 200) return response.data;
    else return [];
}

export const saveMenu = async (saveMenu) => {
    const response = await axios.post<{}>('/api/menu/saveMenu', saveMenu);
    if (response.status === 200) return response.data;
    else return [];
}

export const getParentMenus = async () => {
    const response = await axios.get<{}>('/api/menu/parentMenus', {});
    if (response.status === 200) return response.data;
    else return [];
}

export const getMenu = async (filter) => {
    const response = await axios.post<{}>('/api/menu/menus', filter);
    if (response.status === 200) return response.data;
    else return [];
}

export const saveOPC = async(saveOPC) => {
    const response = await axios.post<{}>('/api/menu/saveOPC', saveOPC);
    if (response.status === 200) return response.data;
    else return [];
}

export const getOPCs = async (menuid, profileid) => {
    const response = await axios.post<{}>('/api/menu/opcs', {id1: menuid, id2:profileid});
    if (response.status === 200) return response.data;
    else return [];
}



export const getOpcsForView = async (id) => {
    const response = await axios.post<{}>('/api/menu/Getopcsforview', {id: id});
    if (response.status === 200) return response.data;
    else return [];
}