import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import {Toaster} from "react-hot-toast";
import './index.css'
import UserForm from './components/adduser/UserForm.jsx';
import Register from './components/adduser/Register.jsx';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    {/* <App />
    <Toaster/> */}
    {/* <UserForm/> */}
    <Register/>
  </StrictMode>,
)
