import React, { useContext, useEffect, useMemo, useState } from 'react'
import RisksTable from '@/components/RisksTable'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { PlusIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'
import RisksForm from '@/components/RisksForm'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { createRisk, updateRisk, getRisksByOrg } from '@/services/risk.service'
import UserContext from '@/context/UserContext'

const Risks = () => {
    const { user } = useContext(UserContext) || {}
    const orgId = user?.id || user?.organization?.id || user?.org?.id || user?.orgId
    const [risks, setRisks] = useState([])
    const [loading, setLoading] = useState(false)

    async function reloadRisks() {
        if (!orgId) return
        try {
            setLoading(true)
            const data = await getRisksByOrg(orgId)
            setRisks(Array.isArray(data) ? data : [])
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        if (orgId) reloadRisks()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [orgId])
    const [statusFilter, setStatusFilter] = useState('All')
    const filtered = useMemo(() => {
        if (statusFilter === 'All') return risks
        return risks.filter(r => String(r.status).toLowerCase() === statusFilter.toLowerCase())
    }, [risks, statusFilter])
    const [addOpen, setAddOpen] = useState(false)
    const [editOpen, setEditOpen] = useState(false)
    const [editing, setEditing] = useState(null)
    return (
        <div>
            <div className='flex flex-row justify-between items-center'>
                <h1 className='text-2xl font-bold text-primary'>Risks</h1>
                <Dialog open={addOpen} onOpenChange={setAddOpen}>
                    <DialogTrigger asChild>
                        <Button aria-label='Add Risk'>
                            <PlusIcon className='size-4' />
                            Add Risk
                        </Button>
                    </DialogTrigger>
                    <DialogContent>
                        <DialogHeader>
                            <DialogTitle>Add Risk</DialogTitle>
                        </DialogHeader>
                        <RisksForm
                            defaultValues={{ category: '', description: '', exposure: '', status: '' }}
                            onSubmit={async (values) => {
                                try {
                                    await createRisk(values)
                                    setAddOpen(false)
                                    await reloadRisks()
                                } catch (e) {
                                    console.error('Failed to create risk', e)
                                }
                            }}
                        />
                    </DialogContent>
                </Dialog>
            </div>
            <div className='mt-4 flex flex-wrap items-center justify-between gap-3'>
                <div className='flex items-center gap-2'>
                    <span className='text-sm text-muted-foreground'>Status</span>
                    <Select value={statusFilter} onValueChange={setStatusFilter}>
                        <SelectTrigger className='h-8 px-2 py-1'>
                            <SelectValue placeholder='All' />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value='All'>All</SelectItem>
                            <SelectItem value='Open'>Open</SelectItem>
                            <SelectItem value='Mitigated'>Mitigated</SelectItem>
                            <SelectItem value='Closed'>Closed</SelectItem>
                        </SelectContent>
                    </Select>
                </div>

            </div>
            <div className='mt-4'>
                <RisksTable
                    data={filtered}
                    showOrgId
                    handleEdit={(row) => {
                        setEditing(row)
                        setEditOpen(true)
                    }}
                />
            </div>
            <Dialog open={editOpen} onOpenChange={setEditOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Edit Risk</DialogTitle>
                    </DialogHeader>
                    <RisksForm
                        defaultValues={editing ?? { category: '', description: '', exposure: '', status: '' }}
                        onSubmit={async (values) => {
                            try {
                                const id = editing?.riskId
                                await updateRisk(id, values)
                                setEditOpen(false)
                                setEditing(null)
                                await reloadRisks()
                            } catch (e) {
                                console.error('Failed to update risk', e)
                            }
                        }}
                    />
                </DialogContent>
            </Dialog>
        </div>
    )
}

export default Risks