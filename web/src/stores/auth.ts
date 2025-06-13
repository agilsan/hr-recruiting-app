import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({ token: '' }),
  actions: {
    setToken(t: string) {
      this.token = t
    }
  }
})
