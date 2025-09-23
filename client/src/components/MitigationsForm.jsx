import React from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { mitigationSchema, MITIGATION_STATUS_VALUES } from '@/schemas/mitigation.schema'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Button } from '@/components/ui/button'
import { Calendar } from '@/components/ui/calendar'
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'

export default function MitigationsForm({ defaultValues, onSubmit }) {
    const form = useForm({
        resolver: zodResolver(mitigationSchema),
        defaultValues: {
            action: defaultValues?.action || '',
            owner: defaultValues?.owner || '',
            deadline: defaultValues?.deadline ? new Date(defaultValues.deadline) : undefined,
            status: defaultValues?.status || 'Open',
        },
        mode: 'onBlur',
    })

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className='space-y-4'>
                <FormField
                    control={form.control}
                    name='action'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Action</FormLabel>
                            <FormControl>
                                <textarea
                                    className='border-input dark:bg-input/30 border bg-transparent rounded-md p-3 text-sm w-full min-h-28 shadow-xs outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]'
                                    placeholder='Describe the mitigation action...'
                                    {...field}
                                />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <FormField
                    control={form.control}
                    name='owner'
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Owner</FormLabel>
                            <FormControl>
                                <Input placeholder='Responsible person' {...field} />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />

                <FormField
                    control={form.control}
                    name='deadline'
                    render={({ field }) => (
                        <FormItem className='flex flex-col'>
                            <FormLabel>Deadline</FormLabel>
                            <Popover>
                                <PopoverTrigger asChild>
                                    <FormControl>
                                        <Button
                                            variant={'outline'}
                                            className={'justify-start text-left font-normal w-full'}
                                        >
                                            {field.value ? field.value.toLocaleDateString() : 'Pick a date'}
                                        </Button>
                                    </FormControl>
                                </PopoverTrigger>
                                <PopoverContent className='w-auto p-0' align='start'>
                                    <Calendar
                                        mode='single'
                                        selected={field.value}
                                        onSelect={field.onChange}
                                        initialFocus
                                    />
                                </PopoverContent>
                            </Popover>
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
                                    {MITIGATION_STATUS_VALUES.map((s) => (
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


