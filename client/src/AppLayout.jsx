import './App.css';
import { SidebarProvider } from '@/components/ui/sidebar';
import AppSidebar from '@/components/AppSidebar';
import { Outlet } from 'react-router';

function AppLayout() {
  return (
    <SidebarProvider>
      <AppSidebar />
      <main className="m-2 w-full overflow-y-auto border border-border p-6 rounded-lg">
        <Outlet />
      </main>
    </SidebarProvider>
  );
}

export default AppLayout;
