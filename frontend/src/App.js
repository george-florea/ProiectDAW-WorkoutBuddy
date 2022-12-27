import React, { useEffect } from 'react';
import {createBrowserRouter, RouterProvider} from 'react-router-dom'
import GeneralLayout from './layouts/GeneralLayout';
import Login from './pages/account/Login';
import Register from './pages/account/Register';
import MainPage from './pages/MainPage';

const router = createBrowserRouter([
  {
      path: '/',
      element: <GeneralLayout />,  
      children: [
        {
          path: '/',
          element: <MainPage/>
        },
        {
          path: 'login',
          element: <Login/>
        },
        {
          path: 'register',
          element: <Register/>
        }
      ]
  },
])

export default function App() {
  return (
    <RouterProvider router={router}>
    </RouterProvider>
  );
};
