import { createBrowserRouter } from 'react-router';
import AppLayout from '@/AppLayout';
import Home from '@/pages/Home';
import AuthLayout from '@/layouts/AuthLayout';
import Login from '@/pages/Login';
import Register from '@/pages/Register';

const router = createBrowserRouter([
  {
    path: '/auth',
    element: <AuthLayout />,
    children: [
      {
        path: '/auth/login',
        element: <Login />,
      },
      {
        path: '/auth/register',
        element: <Register />,
      },
    ],
  },
  {
    path: '/',
    element: <AppLayout />,
    children: [
      {
        path: '/',
        element: <Home />,
      },
    ],
  },
]);

export default router;
