import React, { useEffect } from 'react';
import {createBrowserRouter, RouterProvider} from 'react-router-dom'
import GeneralLayout from './layouts/GeneralLayout';
import Login from './pages/account/Login';
import Register from './pages/account/Register';
import PrivateRoute from './pages/common/PrivateRoute';
import ExercisesLayout from './pages/exercises/ExercisesLayout';
import ExercisesList from './pages/exercises/ExercisesList';
import InsertExercise from './pages/exercises/InsertExercise';
import ViewExercise from './pages/exercises/ViewExercise';
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
          element: <PrivateRoute><ExercisesLayout/></PrivateRoute>,
          children: [
            {
              path: '/exercises',
              element: <PrivateRoute><ExercisesList/></PrivateRoute>
            },
            {
              path:'/exercises/insert-exercise',
              element: <PrivateRoute> <InsertExercise/> </PrivateRoute>
            },
            {
              path: '/exercises/:id',
              element: <PrivateRoute><ViewExercise /></PrivateRoute>
            }
          ]
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
