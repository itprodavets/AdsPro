<template>
  <div class="user-row">
    <div class="user-info" @click="toggleEdit">
      <span>{{ user.username }}</span>
      <span>{{ user.isActive ? 'Yes' : 'No' }}</span>
    </div>
    <div v-if="isEditing" class="edit-panel">
      <div>Username: {{ user.username }}</div>
      <label>
        Active: <input v-model="isActive" type="checkbox"/>
      </label>
      <button @click="saveChanges">OK</button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import {ref} from 'vue';
import {useUsersStore} from '@/stores/usersStore';
import {User} from "@/types";

const props = defineProps<{ user: User }>();
const usersStore = useUsersStore();

const isEditing = ref(false);
const isActive = ref(props.user.isActive);

const toggleEdit = () => {
  isEditing.value = !isEditing.value;
};

const saveChanges = () => {
  usersStore.modifyUser(props.user.username, isActive.value);
  isEditing.value = false;
};
</script>

<style scoped>
.user-row {
  display: flex;
  flex-direction: column;
  margin-bottom: 10px;
}

.user-info {
  display: flex;
  justify-content: space-between;
  cursor: pointer;
}

.edit-panel {
  margin-top: 5px;
  label {
    display: block;
    margin-bottom: 10px;
  }
}
</style>