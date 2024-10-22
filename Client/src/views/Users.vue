<template>
  <div class="users-page">
    <h1>Users Administration</h1>
    <div v-if="usersStore.modifiedUsers.length > 0" class="save-panel">
      One or more users have been modified.
      <button @click="saveChanges">Save</button>
    </div>
    <div class="users-list">
      <UserRow v-for="user in usersStore.users" :key="user.id" :user="user"/>
    </div>
  </div>
</template>

<script lang="ts" setup>
import {onMounted} from 'vue';
import {useUsersStore} from '@/stores/usersStore';
import UserRow from '@/components/UserRow.vue';

const usersStore = useUsersStore();

onMounted(() => {
  usersStore.fetchUsers();
});

const saveChanges = () => {
  usersStore.saveChanges();
};
</script>

<style scoped>
.users-page {
  max-width: 600px;
  margin: 0 auto;
}

.save-panel {
  background-color: #f9f9f9;
  padding: 10px;
  margin-bottom: 15px;
  border: 1px solid #ddd;
}

.users-list {
  display: flex;
  flex-direction: column;
}
</style>