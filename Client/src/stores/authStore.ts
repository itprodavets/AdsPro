import { defineStore } from 'pinia';
import { login } from '@/services/authService';
import { ref } from 'vue';

export const useAuthStore = defineStore('auth', () => {
    const token = ref<string | null>(localStorage.getItem('token'));
    const errorMessage = ref<string | null>(null);

    const performLogin = async (username: string, password: string) => {
        try {
            const response = await login({
                login: username,
                password: password,
            });
            token.value = response.data.token;
            localStorage.setItem('token', response.data.token);
            errorMessage.value = null;
        } catch (error) {
            errorMessage.value = 'Invalid username or password';
        }
    };

    const logout = () => {
        token.value = null;
        localStorage.removeItem('token');
    };

    const isAuthenticated = () => !!token.value;

    return {
        token,
        errorMessage,
        performLogin,
        logout,
        isAuthenticated,
    };
});