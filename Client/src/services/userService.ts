import apiClient from '@/services/apiClient';
import {UpdateUser, User, PagedResult} from '@types';
import {AxiosResponse} from "axios";

const getUsers = async (): Promise<AxiosResponse<Promise<PagedResult<User>>>>  => await apiClient.get<PagedResult<User>>('/api/users/all');
const updateUser = async (command: UpdateUser) : Promise<AxiosResponse<true>>  => await apiClient.post(`/api/users/update`, command);
const meUser = async (): Promise<AxiosResponse<string>> => await apiClient.get('/api/users/me');

export const userService = {
    getUsers,
    updateUser,
    meUser
};
