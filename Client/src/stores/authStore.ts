import {defineStore} from 'pinia';
import {ref} from 'vue';
import {authService} from '@/services/authService';
import {UserLoginRequest} from "@types";

export const useAuthStore = defineStore('auth', () => {
    const token = ref<string | null>(localStorage.getItem('token'));
    const errorMessage = ref<string | null>(null);

    const performLogin = async (request: UserLoginRequest): Promise<boolean> => {
        try {
            const response = await authService.login(request);
            if (response.status === 200 && response.data.token) {
                token.value = response.data.token;
                localStorage.setItem('token', response.data.token);
                errorMessage.value = null;
                return true;
            } else {
                errorMessage.value = 'Invalid username or password';
                return false;
            }
        } catch (error) {
            errorMessage.value = 'An unexpected error occurred';
            return false;
        }
    };

    const logout = () => {
        token.value = null;
        localStorage.removeItem('token');
    };
    
    const isAuthenticated = (): boolean => !!token.value;

    return {
        token,
        errorMessage,
        performLogin,
        logout,
        isAuthenticated
    };
});
