import React from 'react';
import { Mail, Lock, User } from 'lucide-react';
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { registerSchema } from '@/schemas/auth.schema';
import { Button } from '@/components/ui/button';
import { Link } from 'react-router';
import RiskLensLogo from '@/assets/Risklens_Logo.png';

const Register = () => {
  const form = useForm({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      confirmPassword: '',
    },
  });

  const onSubmit = (values) => {
    console.log('register submit', values);
  };

  return (
    <div className="flex flex-col items-center justify-center grow">
      <div className="flex flex-col justify-center relative gap-6 border border-border rounded-lg p-8">
        <img src={RiskLensLogo} alt="RiskLens" className="w-10" />
        <div className="w-full">
          <h1 className="text-2xl w-full font-semibold text-popover-foreground">
            Welcome Aboard!
          </h1>
          <p className="text-muted-foreground text-sm">
            We are happy to have you onboard.
          </p>
        </div>

        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex flex-col items-center justify-center gap-2 w-full"
          >
            <FormField
              control={form.control}
              name="firstName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-muted-foreground text-xs">
                    First name
                  </FormLabel>
                  <div className="relative">
                    <FormControl>
                      <Input className="pl-9 w-84" {...field} />
                    </FormControl>
                    <User
                      aria-hidden
                      className="absolute left-2 top-1/2 -translate-y-1/2 size-4 text-muted-foreground"
                    />
                  </div>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="lastName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-muted-foreground text-xs">
                    Last name
                  </FormLabel>
                  <div className="relative">
                    <FormControl>
                      <Input className="pl-9 w-84" {...field} />
                    </FormControl>
                    <User
                      aria-hidden
                      className="absolute left-2 top-1/2 -translate-y-1/2 size-4 text-muted-foreground"
                    />
                  </div>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-muted-foreground text-xs">
                    Email
                  </FormLabel>
                  <div className="relative">
                    <FormControl>
                      <Input type="email" className="pl-9 w-84" {...field} />
                    </FormControl>
                    <Mail
                      aria-hidden
                      className="absolute left-2 top-1/2 -translate-y-1/2 size-4 text-muted-foreground"
                    />
                  </div>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-muted-foreground text-xs">
                    Password
                  </FormLabel>
                  <div className="relative">
                    <FormControl>
                      <Input type="password" className="pl-9 w-84" {...field} />
                    </FormControl>
                    <Lock
                      aria-hidden
                      className="absolute left-2 top-1/2 -translate-y-1/2 size-4 text-muted-foreground"
                    />
                  </div>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="confirmPassword"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-muted-foreground text-xs">
                    Confirm password
                  </FormLabel>
                  <div className="relative">
                    <FormControl>
                      <Input type="password" className="pl-9 w-84" {...field} />
                    </FormControl>
                    <Lock
                      aria-hidden
                      className="absolute left-2 top-1/2 -translate-y-1/2 size-4 text-muted-foreground"
                    />
                  </div>
                  <FormMessage />
                </FormItem>
              )}
            />
            <p className="text-left w-full text-muted-foreground text-sm">
              Already have an account?{' '}
              <Link to="/auth/login" className="text-primary">
                Login
              </Link>
            </p>
            <Button type="submit" className="w-full">
              Create account
            </Button>
          </form>
        </Form>
      </div>
    </div>
  );
};

export default Register;
