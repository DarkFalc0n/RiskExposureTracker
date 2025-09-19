import { z } from 'zod';

export const loginSchema = z.object({
    email: z.email('Please enter a valid email address'),
    password: z
        .string({ required_error: 'Password is required' })
        .min(8, 'Use at least 8 characters'),
});

export const registerSchema = z
    .object({
        firstName: z
            .string({ required_error: 'First name is required' })
            .trim()
            .min(2, 'First name must be at least 2 characters'),
        lastName: z
            .string({ required_error: 'Last name is required' })
            .trim()
            .min(2, 'Last name must be at least 2 characters'),
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
