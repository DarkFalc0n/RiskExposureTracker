import React, { useMemo, useState } from 'react'
import { ArrowUpDownIcon, ArrowUpIcon, ArrowDownIcon, PencilIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'

function formatDate(value) {
    if (!value) return '—'
    const d = value instanceof Date ? value : new Date(value)
    if (isNaN(d.getTime())) return '—'
    return d.toLocaleDateString()
}

function StatusBadge({ status }) {
    const normalized = String(status || '').toLowerCase()
    const styles = {
        open: 'bg-amber-100 text-amber-800 dark:bg-amber-400/20 dark:text-amber-300',
        completed: 'bg-emerald-100 text-emerald-800 dark:bg-emerald-400/20 dark:text-emerald-300',
        default: 'bg-zinc-100 text-zinc-800 dark:bg-zinc-400/20 dark:text-zinc-200',
    }
    const cls = styles[normalized] || styles.default
    return (
        <span className={`inline-flex items-center px-2 py-0.5 rounded-md text-xs font-medium ${cls}`}>{status}</span>
    )
}

export default function MitigationsTable({ data = [], onEdit = () => { }, statusSort = 'none', onStatusSortChange }) {
    // riskId sort: internal toggle via column header (none -> asc -> desc -> none)
    const [riskSort, setRiskSort] = useState('none')
    const SortRiskIcon = riskSort === 'asc' ? ArrowUpIcon : riskSort === 'desc' ? ArrowDownIcon : ArrowUpDownIcon

    const isStatusControlled = typeof onStatusSortChange === 'function'
    const [internalStatusSort, setInternalStatusSort] = useState(statusSort)
    const statusOrder = isStatusControlled ? statusSort : internalStatusSort // 'none' | 'openFirst' | 'completedFirst'

    const sortedData = useMemo(() => {
        let rows = Array.isArray(data) ? [...data] : []

        // Apply status sort
        if (statusOrder === 'openFirst' || statusOrder === 'completedFirst') {
            const openRank = statusOrder === 'openFirst' ? 0 : 1
            const completedRank = statusOrder === 'openFirst' ? 1 : 0
            const rank = (s) => String(s || '').toLowerCase() === 'open' ? openRank : completedRank
            rows.sort((a, b) => rank(a.status) - rank(b.status))
        }

        // Apply riskId sort
        if (riskSort === 'asc') {
            rows.sort((a, b) => Number(a.riskId || 0) - Number(b.riskId || 0))
        } else if (riskSort === 'desc') {
            rows.sort((a, b) => Number(b.riskId || 0) - Number(a.riskId || 0))
        }

        return rows
    }, [data, statusOrder, riskSort])

    function toggleRiskSort() {
        setRiskSort((prev) => prev === 'asc' ? 'desc' : prev === 'desc' ? 'none' : 'asc')
    }

    function toggleStatusSort() {
        const next = statusOrder === 'openFirst' ? 'completedFirst' : statusOrder === 'completedFirst' ? 'none' : 'openFirst'
        if (isStatusControlled) onStatusSortChange(next)
        else setInternalStatusSort(next)
    }

    const SortStatusIcon = statusOrder === 'openFirst' ? ArrowUpIcon : statusOrder === 'completedFirst' ? ArrowDownIcon : ArrowUpDownIcon

    return (
        <div className="border border-border rounded-lg overflow-x-auto">
            <table className="w-full text-sm">
                <thead>
                    <tr className="bg-muted/50 text-left">
                        <th className="px-2 py-3 font-medium text-zinc-600">
                            <button type="button" className="inline-flex items-center gap-1 px-2 py-1 rounded hover:bg-accent hover:text-accent-foreground transition-colors" onClick={toggleRiskSort} title="Sort by Risk ID">
                                <span className="pl-2 pr-1">Risk ID</span>
                                <SortRiskIcon className="size-4" />
                            </button>
                        </th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Action</th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Owner</th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Deadline</th>
                        <th className="px-2 py-3 font-medium text-zinc-600">
                            <button type="button" className="inline-flex items-center gap-1 px-2 py-1 rounded hover:bg-accent hover:text-accent-foreground transition-colors" onClick={toggleStatusSort} title="Sort by Status">
                                <span className="pl-2 pr-1">Status</span>
                                <SortStatusIcon className="size-4" />
                            </button>
                        </th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {sortedData.length === 0 && (
                        <tr>
                            <td className="px-4 py-6 text-center text-zinc-500" colSpan={6}>No mitigations found.</td>
                        </tr>
                    )}
                    {sortedData.map((row) => (
                        <tr key={`${row.riskId}-${row.mitigationId || row.id || row.action}-${row.deadline}`} className="border-t border-border">
                            <td className="px-4 py-3 align-top text-zinc-800 dark:text-zinc-100">{row.riskId}</td>
                            <td className="px-4 py-3 align-top max-w-[28rem]"><p className="truncate" title={row.action}>{row.action}</p></td>
                            <td className="px-4 py-3 align-top">{row.owner}</td>
                            <td className="px-4 py-3 align-top">{formatDate(row.deadline)}</td>
                            <td className="px-4 py-3 align-top"><StatusBadge status={row.status} /></td>
                            <td className="px-4 py-3 align-top">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="h-8 w-8"
                                    aria-label={`Edit mitigation for risk ${row.riskId}`}
                                    onClick={() => onEdit(row)}
                                    title="Edit mitigation"
                                >
                                    <PencilIcon className="size-4" />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}


