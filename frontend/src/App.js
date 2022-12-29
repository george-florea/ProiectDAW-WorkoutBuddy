import React, { useEffect } from 'react';
import {createBrowserRouter, RouterProvider} from 'react-router-dom'
import GeneralLayout from './layouts/GeneralLayout';
import Login from './pages/account/Login';
import Register from './pages/account/Register';
import PrivateRoute from './pages/common/PrivateRoute';
import ExercisesList from './pages/exercises/ExercisesList';
import MainPage from './pages/MainPage';

const router = createBrowserRouter([
  {
      path: '/',
      element: <GeneralLayout />,  
      children: [
        {
          path: '/',
          element: <PrivateRoute><MainPage/></PrivateRoute>
        },
        {
          path: 'login',
          element: <Login/>
        },
        {
          path: 'register',
          element: <Register/>
        },
        {
          path: 'exercises',
          element: <PrivateRoute><ExercisesList/></PrivateRoute>
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
