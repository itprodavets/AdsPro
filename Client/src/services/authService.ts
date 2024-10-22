import apiClient from "./apiClient.ts";
import {UserLoginRequest, UserLoginResponse} from "@/types";

async function login(request: UserLoginRequest) {
     return await apiClient.post<UserLoginResponse>('/api/auth/login', request, {
        withCredentials: true
    });
}

export const authService = {
    login,
};