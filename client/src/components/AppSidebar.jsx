import {
    Sidebar,
    SidebarContent,
    SidebarFooter,
    SidebarGroup,
    SidebarHeader,
} from "@/components/ui/sidebar"
import RiskLensLogo from "@/assets/Risklens_Logo_Dark.png"
import { Button } from "@/components/ui/button"
import { SidebarMenuItem, SidebarMenuButton } from "@/components/ui/sidebar"
import { Avatar, AvatarFallback } from "@radix-ui/react-avatar"
import { logout } from "@/services/auth.service"
import { Menu, FileWarning, ShieldCheck, FileText, User } from "lucide-react"
import { Link } from "react-router"
import { useContext } from "react"
import UserContext from "@/context/UserContext"

export default function AppSidebar() {
    const { user } = useContext(UserContext) || {}
    const displayName = (user?.name || user?.organization?.name || user?.org?.name || "User").trim()
    const email = (user?.email || user?.organization?.contactEmail || "").trim()
    const initial = (displayName[0] || "U").toUpperCase()

    const items = [
        {
            title: "Dashboard",
            url: "/",
            icon: Menu,
        },
        {
            title: "Risks",
            url: "/risks",
            icon: FileWarning,
        },
        {
            title: "Mitigations",
            url: "/mitigations",
            icon: ShieldCheck,
        },
        {
            title: "Reports",
            url: "/reports",
            icon: FileText,
        },
        {
            title: "Profile",
            url: "/profile",
            icon: User,
        }
    ]
    return (
        <div className="dark">
            <Sidebar collapsible="none">
                <SidebarHeader className="items-center justify-center pt-10 flex flex-row gap-4">
                    <img src={RiskLensLogo} alt="RiskLens" className="w-10" />
                    <h1 className="text-2xl font-bold">RiskLens</h1>
                </SidebarHeader>
                <SidebarContent className="mt-10">
                    {items.map((item) => (
                        <SidebarMenuItem key={item.title} className="px-4">
                            <SidebarMenuButton asChild>
                                <Link to={item.url}>
                                    <item.icon />
                                    {item.title}
                                </Link>
                            </SidebarMenuButton>
                        </SidebarMenuItem>
                    ))}
                </SidebarContent>
                <SidebarFooter className="border-t border-zinc-600 py-4 flex flex-col gap-6">
                    <div className="flex flex-row gap-2 justify-start px-4">
                        <div className="flex flex-col justify-center h-full">
                            <Avatar className="w-10 h-10 flex flex-col justify-center">
                                <AvatarFallback>{initial}</AvatarFallback>
                            </Avatar>
                        </div>
                        <div className="flex flex-col gap-1">
                            <p className="text-sm">{displayName}</p>
                            <p className="text-xs text-zinc-400">{email}</p>
                        </div>
                    </div>
                    <Button className="w-auto" onClick={logout}>Logout</Button>
                </SidebarFooter>
            </Sidebar>
        </div>
    )
}