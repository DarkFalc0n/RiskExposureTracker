import { z } from 'zod';

const REGION_VALUES = [
    'NorthAmerica',
    'SouthAmerica',
    'Europe',
    'MiddleEast',
    'Africa',
    'AsiaPacific',
];

export const loginSchema = z.object({
    email: z.email('Please enter a valid email address'),
    password: z
        .string({ required_error: 'Password is required' })
        .min(8, 'Use at least 8 characters'),
});

export const registerSchema = z
    .object({
        name: z
            .string({ required_error: 'Organization name is required' })
            .trim()
            .min(2, 'Name must be at least 2 characters'),
        sector: z
            .string({ required_error: 'Sector is required' })
            .trim()
            .min(2, 'Sector must be at least 2 characters'),
        region: z.preprocess(
            (v) => (v == null ? '' : v),
            z
                .string({
                    required_error: 'Region is required',
                    invalid_type_error: 'Region is required',
                })
                .min(1, 'Region is required')
                .refine((val) => REGION_VALUES.includes(val), {
                    message: 'Invalid region selected',
                })
        ),
        contact: z
            .string({ required_error: 'Primary contact is required' })
            .trim()
            .min(2, 'Contact must be at least 2 characters'),
        email: z.email('Please enter a valid email address'),
        password: z
            .string({ required_error: 'Password is required' })
            .min(8, 'Use at least 8 characters'),
        confirmPassword: z
            .string({ required_error: 'Please confirm your password' })
            .min(8, 'Use at least 8 characters'),
    })
    .refine((data) => data.password === data.confirmPassword, {
        path: ['confirmPassword'],
        message: "Your passwords don't match",
    });
