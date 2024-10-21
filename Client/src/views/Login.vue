<template>
  <div class="login-form">
    <form @submit.prevent="handleLogin">
      <div class="form-group">
        <label for="username">Username</label>
        <input
            type="text"
            id="username"
            v-model="username"
            required
        />
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input
            type="password"
            id="password"
            v-model="password"
            required
        />
      </div>
      <button type="submit">Login</button>
      <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useAuthStore } from '@/stores/authStore';

export default defineComponent({
  name: 'LoginForm',
  setup() {
    const authStore = useAuthStore();
    
    const username = ref('');
    const password = ref('');
    
    const handleLogin = async () => {
      await authStore.performLogin(username.value, password.value);
      if (authStore.isAuthenticated()) {
        window.location.href = '/welcome';
      }
    };

    return {
      username,
      password,
      handleLogin,
      errorMessage: authStore.errorMessage,
    };
  },
});
</script>

<style scoped>
.login-form {
  max-width: 300px;
  margin: 0 auto;
}

.form-group {
  margin-bottom: 15px;
}

label {
  margin-right: 0.8em;
}

.error {
  color: red;
  margin-top: 10px;
}
</style>
