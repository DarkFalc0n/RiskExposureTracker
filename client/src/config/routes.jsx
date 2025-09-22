import { createBrowserRouter } from 'react-router';
import AppLayout from '@/AppLayout';
import Home from '@/pages/Home';
import AuthLayout from '@/layouts/AuthLayout';
import Login from '@/pages/Login';
import Register from '@/pages/Register';
import { UserProvider } from '@/context/UserContext';
import ProtectedRoute from '@/components/ProtectedRoute';

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
    element: (
      <ProtectedRoute>
        <UserProvider>
          <AppLayout />
        </UserProvider>
      </ProtectedRoute>
    ),
    children: [
      {
        path: '/',
        element: <Home />,
      },
    ],
  },
]);

export default router;
