import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './app/App.tsx'
import { Provider } from 'react-redux'
import { store } from './app/store.ts'
import axios from 'axios'

import './styles/reset.scss'

axios.defaults.baseURL = 'https://localhost:7248/api/';
axios.defaults.withCredentials = true;

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>,
)
