import { defineConfig } from 'vite'
import dotenv from 'dotenv'
import react from '@vitejs/plugin-react'

dotenv.config();

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
  },
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `@use "./src/styles/globals.scss" as *;`
      }
    }
  }
})
