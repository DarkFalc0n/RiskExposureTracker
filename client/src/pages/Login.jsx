import React from 'react';
import { Mail, Lock } from 'lucide-react';
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
import { loginSchema } from '@/schemas/auth.schema';
import { Button } from '@/components/ui/button';
import { Link } from 'react-router';
import RiskLensLogo from '@/assets/Risklens_Logo.png';

const Login = () => {
  const form = useForm({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: '',
      password: '',
    },
  });

  const onSubmit = (values) => {
    console.log('login submit', values);
  };

  return (
    <div className="flex flex-col items-center justify-center grow">
      <div className="flex flex-col justify-center relative gap-6 border border-border rounded-lg p-8">
        <img src={RiskLensLogo} alt="RiskLens" className="w-10" />
        <div className="w-full">
          <h1 className="text-2xl w-full font-semibold text-popover-foreground">
            Welcome back!
          </h1>
          <p className="text-muted-foreground text-sm">
            We are happy to see you again.
          </p>
        </div>

        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex flex-col items-center justify-center gap-2 w-full"
          >
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
            <p className="text-left w-full text-muted-foreground text-sm mt-4">
              New to RiskLens?{' '}
              <Link to="/auth/register" className="text-primary">
                Register
              </Link>
            </p>
            <Button type="submit" className="w-full">
              Login
            </Button>
          </form>
        </Form>
      </div>
    </div>
  );
};

export default Login;
