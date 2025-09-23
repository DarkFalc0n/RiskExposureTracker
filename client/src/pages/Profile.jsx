import React, { useContext } from 'react'
import UserContext from '@/context/UserContext'

const Profile = () => {
    const { user } = useContext(UserContext) || {}
    const org = user?.organization || user?.org || {}

    const details = {
        id: user?.id || org?.id || '',
        name: user?.name || org?.name || '',
        sector: org?.sector || user?.sector || '',
        region: org?.region || user?.region || '',
        contact: user?.contact || org?.contact || '',
        email: user?.email || org?.contactEmail || org?.email || '',
        roles: Array.isArray(user?.roles) ? user.roles : [],
    }

    return (
        <div>
            <h1 className='text-2xl font-bold text-primary'> Organization Profile</h1>
            <div className="mt-4">
                <div className="border border-zinc-300 rounded-lg bg-card p-6 shadow-sm">
                    {(!details.name && !details.email) ? (
                        <p className="text-sm text-muted-foreground">No user information available.</p>
                    ) : (
                        <div className="grid gap-4 md:grid-cols-2">
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Name</p>
                                <p className="text-sm font-medium">{details.name || 'N/A'}</p>
                            </div>
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Email</p>
                                <p className="text-sm font-medium break-all">{details.email || 'N/A'}</p>
                            </div>
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Sector</p>
                                <p className="text-sm font-medium">{details.sector || 'N/A'}</p>
                            </div>
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Region</p>
                                <p className="text-sm font-medium">{details.region || 'N/A'}</p>
                            </div>
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Contact</p>
                                <p className="text-sm font-medium">{details.contact || 'N/A'}</p>
                            </div>
                            <div>
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">Roles</p>
                                <p className="text-sm font-medium">{details.roles.length ? details.roles.join(', ') : 'N/A'}</p>
                            </div>
                            <div className="md:col-span-2">
                                <p className="text-xs uppercase tracking-wide text-muted-foreground">ID</p>
                                <p className="text-sm font-medium break-all">{details.id || 'N/A'}</p>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}

export default Profile