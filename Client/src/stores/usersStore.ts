import {defineStore} from 'pinia';
import {ref} from 'vue';
import {UpdateUser, User} from "@/types";
import {userService} from "@/services/userService.ts";

export const useUsersStore = defineStore('users', () => {
    const total = ref<number>(0);
    const users = ref<Array<User>>([]);
    const modifiedUsers = ref<Array<UpdateUser>>([]);

    const fetchUsers = async () => {
        try {
            const data = await userService.getUsers();
            
            total.value = data.total;
            users.value = data.items;
        } catch (error) {
            console.error('Failed to fetch users:', error);
        }
    };

    const modifyUser = (username: string, isActive: boolean) => {
        const existingIndex = modifiedUsers.value.findIndex((user) => user.username === username);
        if (existingIndex !== -1) {
            modifiedUsers.value[existingIndex].username = username;
            modifiedUsers.value[existingIndex].isActive = isActive;
        } else {
            modifiedUsers.value.push({username, isActive});
        }
    };

    const saveChanges = async () => {
        try {
            for (const user of modifiedUsers.value) {
                alert('Saving changes for user: ' + user.username);
                await userService.updateUser({
                    username: user.username,
                    isActive: user.isActive
                });
            }
            await fetchUsers();
            modifiedUsers.value = [];
        } catch (error) {
            console.error('Failed to save changes:', error);
        }
    };

    return {
        users,
        modifiedUsers,
        fetchUsers,
        modifyUser,
        saveChanges,
    };
});
