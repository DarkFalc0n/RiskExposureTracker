import { createBrowserRouter } from 'react-router';
import AppLayout from '@/AppLayout';
import Home from '@/pages/Home';

const router = createBrowserRouter([
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
