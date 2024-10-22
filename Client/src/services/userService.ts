import apiClient from '@/services/apiClient';
import {UpdateUser, User, PagedResult} from '@types';

const getUsers = async (): Promise<PagedResult<User>>  => await apiClient.get<PagedResult<User>>('/api/users/all');
const updateUser = async (command: UpdateUser) : Promise<true>  => await apiClient.post(`/api/users/update`, command);

export const userService = {
    getUsers,
    updateUser,
};
