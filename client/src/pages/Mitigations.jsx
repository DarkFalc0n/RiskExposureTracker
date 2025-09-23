import React, { useContext, useEffect, useMemo, useState } from 'react'
import MitigationsTable from '@/components/MitigationsTable'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import MitigationsForm from '@/components/MitigationsForm'
import { getMitigationsByOrg } from '@/services/mitigation.service'
import UserContext from '@/context/UserContext'

const Mitigations = () => {
    const { user } = useContext(UserContext) || {}
    const orgId = user?.id || user?.organization?.id || user?.org?.id || user?.orgId

    const [mitigations, setMitigations] = useState([])
    const [statusSort, setStatusSort] = useState('none')
    const [editOpen, setEditOpen] = useState(false)
    const [editing, setEditing] = useState(null)

    useEffect(() => {
        async function load() {
            if (!orgId) return
            const data = await getMitigationsByOrg(orgId)
            setMitigations(Array.isArray(data) ? data : [])
        }
        load()
    }, [orgId])

    const filtered = useMemo(() => mitigations, [mitigations])

    return (
        <div>
            <h1 className='text-2xl font-bold text-primary'>Mitigations</h1>
            <div className='mt-4 flex items-center gap-2'>
                <span className='text-sm text-muted-foreground'>Status</span>
                <Select value={statusSort} onValueChange={setStatusSort}>
                    <SelectTrigger className='h-8 px-2 py-1'>
                        <SelectValue placeholder='None' />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value='none'>None</SelectItem>
                        <SelectItem value='openFirst'>Open first</SelectItem>
                        <SelectItem value='completedFirst'>Completed first</SelectItem>
                    </SelectContent>
                </Select>
            </div>
            <div className='mt-4'>
                <MitigationsTable
                    data={filtered}
                    statusSort={statusSort}
                    onStatusSortChange={setStatusSort}
                    onEdit={(row) => {
                        setEditing(row)
                        setEditOpen(true)
                    }}
                />
            </div>
            <Dialog open={editOpen} onOpenChange={setEditOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Edit Mitigation</DialogTitle>
                    </DialogHeader>
                    <MitigationsForm
                        defaultValues={editing ?? { action: '', owner: '', deadline: undefined, status: 'Open' }}
                        onSubmit={async (values) => {
                            try {
                                // Wire edit submit using updateMitigation service
                                // RiskId stays the same; backend validates ownership
                                // eslint-disable-next-line no-unused-vars
                                const { mitigationId, id, riskId, ...rest } = { ...editing, ...values }
                                // Prefer mitigationId, fallback to id
                                const targetId = editing?.mitigationId ?? editing?.id
                                if (!targetId) {
                                    console.error('Missing mitigation id')
                                    return
                                }
                                const { updateMitigation } = await import('@/services/mitigation.service')
                                await updateMitigation(targetId, { ...rest, riskId: editing?.riskId })
                                setEditOpen(false)
                                setEditing(null)
                                // Refresh list
                                const data = await getMitigationsByOrg(orgId)
                                setMitigations(Array.isArray(data) ? data : [])
                            } catch (e) {
                                console.error('Failed to update mitigation', e)
                            }
                        }}
                    />
                </DialogContent>
            </Dialog>
        </div>
    )
}

export default Mitigations