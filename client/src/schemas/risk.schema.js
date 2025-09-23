import { z } from 'zod';

export const RISK_STATUS_VALUES = ['Open', 'Mitigated', 'Closed'];
export const RISK_CATEGORY_VALUES = [
    'Property',
    'Liability',
    'Operational',
    'Financial',
    'Cyber',
    'HumanCapital',
    'ComplianceAndRegulatory',
    'Strategic',
    'Other',
];

export const riskSchema = z.object({
    category: z
        .string({ required_error: 'Category is required' })
        .min(1, 'Category is required')
        .refine((val) => RISK_CATEGORY_VALUES.includes(val), 'Invalid category selected'),
    description: z
        .string({ required_error: 'Description is required' })
        .trim()
        .min(10, 'Description must be at least 10 characters')
        .max(500, 'Description must be 500 characters or less'),
    exposure: z
        .preprocess((v) => (v === '' || v == null ? undefined : Number(v)), z
            .number({ invalid_type_error: 'Exposure must be a number', required_error: 'Exposure is required' })
            .nonnegative('Exposure must be zero or positive')
        ),
    status: z
        .string({ required_error: 'Status is required' })
        .min(1, 'Status is required')
        .refine((val) => RISK_STATUS_VALUES.includes(val), 'Invalid status selected'),
});


