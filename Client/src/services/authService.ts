import apiClient from "./apiClient.ts";


type UserLoginRequest = {
    username: string;
    password: string;
};

export async function login(request: UserLoginRequest) {
    return apiClient.post('/auth/login', request);
}