import { createBrowserRouter } from 'react-router';
import AppLayout from '@/AppLayout';
import Dashboard from '@/pages/Dashboard';
import AuthLayout from '@/layouts/AuthLayout';
import Login from '@/pages/Login';
import Register from '@/pages/Register';
import { UserProvider } from '@/context/UserContext';
import ProtectedRoute from '@/components/ProtectedRoute';
import Risks from '@/pages/Risks';
import Mitigations from '@/pages/Mitigations';
import Reports from '@/pages/Reports';
import Profile from '@/pages/Profile';

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
        element: <Dashboard />,
      },
      {
        path: '/risks',
        element: <Risks />,
      },
      {
        path: '/mitigations',
        element: <Mitigations />,
      },
      {
        path: '/reports',
        element: <Reports />,
      },
      {
        path: '/profile',
        element: <Profile />,
      },
    ],
  },
]);

export default router;
