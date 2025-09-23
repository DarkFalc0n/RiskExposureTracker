import React, { useEffect, useState } from 'react'
import { getUser } from '@/services/auth.service'

const UserContext = React.createContext()

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null)

    useEffect(() => {
        (async () => {
            try {
                const data = await getUser()
                setUser(data ?? null)
            } catch {
                setUser(null)
            }
        })()
    }, [])

    return (
        <UserContext.Provider value={{ user }}>
            {children}
        </UserContext.Provider>
    )
}

export default UserContext