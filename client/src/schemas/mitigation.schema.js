import { z } from 'zod';

export const MITIGATION_STATUS_VALUES = ['Open', 'Completed'];

export const mitigationSchema = z.object({
    // riskId will be injected from the caller, not a visible field in the form
    action: z
        .string({ required_error: 'Action is required' })
        .trim()
        .min(10, 'Action must be at least 10 characters')
        .max(200, 'Action must be 200 characters or less'),
    owner: z
        .string({ required_error: 'Owner is required' })
        .trim()
        .min(2, 'Owner must be at least 2 characters')
        .max(100, 'Owner must be 100 characters or less'),
    deadline: z
        .preprocess((v) => (typeof v === 'string' || v instanceof Date ? new Date(v) : v), z
            .date({ required_error: 'Deadline is required', invalid_type_error: 'Invalid date' })
        )
        .refine((d) => {
            const today = new Date();
            today.setHours(0, 0, 0, 0)
            return d >= today
        }, { message: 'Deadline cannot be in the past' }),
    status: z
        .string({ required_error: 'Status is required' })
        .min(1, 'Status is required')
        .refine((val) => MITIGATION_STATUS_VALUES.includes(val), 'Invalid status selected'),
});


