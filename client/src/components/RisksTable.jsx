import React, { useMemo, useState } from 'react'
import { ArrowUpDownIcon, ArrowUpIcon, ArrowDownIcon, PencilIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'

function formatCurrency(value) {
    if (value == null || isNaN(Number(value))) return '—'
    return new Intl.NumberFormat(undefined, { style: 'currency', currency: 'USD', maximumFractionDigits: 2 }).format(Number(value))
}

function StatusBadge({ status }) {
    const normalized = String(status || '').toLowerCase()
    const styles = {
        open: 'bg-amber-100 text-amber-800 dark:bg-amber-400/20 dark:text-amber-300',
        mitigated: 'bg-sky-100 text-sky-800 dark:bg-sky-400/20 dark:text-sky-300',
        closed: 'bg-emerald-100 text-emerald-800 dark:bg-emerald-400/20 dark:text-emerald-300',
        default: 'bg-zinc-100 text-zinc-800 dark:bg-zinc-400/20 dark:text-zinc-200',
    }
    const cls = styles[normalized] || styles.default
    return (
        <span className={`inline-flex items-center px-2 py-0.5 rounded-md text-xs font-medium ${cls}`}>{status}</span>
    )
}

export default function RisksTable({ data = [], showOrgId = false, exposureSort = 'none', onExposureSortChange, handleEdit = () => { } }) {
    const isControlled = typeof onExposureSortChange === 'function'
    const [internalSort, setInternalSort] = useState(exposureSort)
    const sortOrder = isControlled ? exposureSort : internalSort

    const sortedData = useMemo(() => {
        if (!Array.isArray(data)) return []
        if (sortOrder === 'asc') {
            return [...data].sort((a, b) => Number(a.exposure || 0) - Number(b.exposure || 0))
        }
        if (sortOrder === 'desc') {
            return [...data].sort((a, b) => Number(b.exposure || 0) - Number(a.exposure || 0))
        }
        return data
    }, [data, sortOrder])

    function toggleSort() {
        const next = sortOrder === 'asc' ? 'desc' : sortOrder === 'desc' ? 'none' : 'asc'
        if (isControlled) onExposureSortChange(next)
        else setInternalSort(next)
    }

    const SortIcon = sortOrder === 'asc' ? ArrowUpIcon : sortOrder === 'desc' ? ArrowDownIcon : ArrowUpDownIcon

    return (
        <div className="border border-border rounded-lg overflow-x-auto">
            <table className="w-full text-sm">
                <thead>
                    <tr className="bg-muted/50 text-left">
                        <th className="px-4 py-3 font-medium text-zinc-600">Risk ID</th>
                        {showOrgId && <th className="px-4 py-3 font-medium text-zinc-600">Org ID</th>}
                        <th className="px-4 py-3 font-medium text-zinc-600">Category</th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Description</th>
                        <th className="px-2 py-3 font-medium text-zinc-600">
                            <button type="button" className="inline-flex items-center gap-1 px-2 py-1 rounded hover:bg-accent hover:text-accent-foreground transition-colors" onClick={toggleSort} title="Sort by exposure">
                                <span className="pl-2 pr-1">Exposure</span>
                                <SortIcon className="size-4" />
                            </button>
                        </th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Status</th>
                        <th className="px-4 py-3 font-medium text-zinc-600">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {sortedData.length === 0 && (
                        <tr>
                            <td className="px-4 py-6 text-center text-zinc-500" colSpan={showOrgId ? 7 : 6}>No risks found.</td>
                        </tr>
                    )}
                    {sortedData.map((row) => (
                        <tr key={`${row.orgId}-${row.riskId}`} className="border-t border-border">
                            <td className="px-4 py-3 align-top text-zinc-800 dark:text-zinc-100">{row.riskId}</td>
                            {showOrgId && (
                                <td className="px-4 py-3 align-top text-zinc-800 dark:text-zinc-100">
                                    <span className="font-mono text-xs break-all">{row.orgId}</span>
                                </td>
                            )}
                            <td className="px-4 py-3 align-top text-zinc-800 dark:text-zinc-100">{row.category}</td>
                            <td className="px-4 py-3 align-top max-w-[28rem]">
                                <p className="text-zinc-800 dark:text-zinc-100 truncate" title={row.description}>{row.description}</p>
                            </td>
                            <td className="px-4 py-3 align-top text-zinc-800 dark:text-zinc-100">{formatCurrency(row.exposure)}</td>
                            <td className="px-4 py-3 align-top"><StatusBadge status={row.status} /></td>
                            <td className="px-4 py-3 align-top">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="h-8 w-8"
                                    aria-label={`Edit risk ${row.riskId}`}
                                    onClick={() => handleEdit(row)}
                                    title="Edit risk"
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


