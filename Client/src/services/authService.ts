import apiClient from "./apiClient.ts";


type UserLoginRequest = {
    username: string;
    password: string;
};

 async function login(request: UserLoginRequest) {
    return await apiClient.post('/auth/login', request, {
        withCredentials: true
    });
}

export const authService = {
    login,
};