import apiClient from "./apiClient.ts";
import {UserLoginRequest, UserLoginResponse} from "@/types";
import {AxiosResponse} from "axios";

 const login = async (request: UserLoginRequest): Promise<AxiosResponse<UserLoginResponse>> => {
     return await apiClient.post<UserLoginResponse>('/api/auth/login', request, {
        withCredentials: true
    });
}

export const authService = {
    login,
};