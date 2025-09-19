import { z } from 'zod';

export const loginSchema = z.object({
    email: z.email('Enter a valid email'),
    password: z.string().min(8, 'Min 8 characters'),
});

export const registerSchema = z.object({
    firstName: z.string().min(2, 'Too short'),
    lastName: z.string().min(2, 'Too short'),
    email: z.email(),
    password: z.string().min(8),
    confirmPassword: z.string().min(8),
}).refine((data) => data.password === data.confirmPassword, {
    path: ['confirmPassword'],
    message: 'Passwords do not match',
});