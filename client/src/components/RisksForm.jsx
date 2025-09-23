import React from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { riskSchema, RISK_CATEGORY_VALUES, RISK_STATUS_VALUES } from '@/schemas/risk.schema'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Button } from '@/components/ui/button'

export default function RisksForm({ defaultValues, onSubmit }) {
    const form = useForm({
        resolver: zodResolver(riskSchema),
        defaultValues: {
            category: defaultValues?.category || '',
            description: defaultValues?.description || '',
            exposure: defaultValues?.exposure ?? '',
            status: defaultValues?.status || '',
        },
        mode: 'onBlur',
    })

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className='space-y-4'>
                <FormField
                    control={form.control}
                    name='category'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Category</FormLabel>
                            <Select value={field.value} onValueChange={field.onChange}>
                                <FormControl>
                                    <SelectTrigger className='w-full'>
                                        <SelectValue placeholder='Select category' />
                                    </SelectTrigger>
                                </FormControl>
                                <SelectContent>
                                    {RISK_CATEGORY_VALUES.map((c) => (
                                        <SelectItem key={c} value={c}>{c}</SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <FormField
                    control={form.control}
                    name='description'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Description</FormLabel>
                            <FormControl>
                                <textarea
                                    className='border-input dark:bg-input/30 border bg-transparent rounded-md p-3 text-sm w-full min-h-28 shadow-xs outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]'
                                    placeholder='Describe the risk...'
                                    {...field}
                                />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <FormField
                    control={form.control}
                    name='exposure'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Exposure (USD)</FormLabel>
                            <FormControl>
                                <Input type='number' step='0.01' placeholder='0.00' {...field} />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <FormField
                    control={form.control}
                    name='status'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Status</FormLabel>
                            <Select value={field.value} onValueChange={field.onChange}>
                                <FormControl>
                                    <SelectTrigger className='w-full'>
                                        <SelectValue placeholder='Select status' />
                                    </SelectTrigger>
                                </FormControl>
                                <SelectContent>
                                    {RISK_STATUS_VALUES.map((s) => (
                                        <SelectItem key={s} value={s}>{s}</SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <div className='pt-2'>
                    <Button type='submit' className='w-full'>Submit</Button>
                </div>
            </form>
        </Form>
    )
}


