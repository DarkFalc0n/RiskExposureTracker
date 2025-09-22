import React from 'react';
import { Outlet } from 'react-router';
import { AuroraBackground } from '@/components/ui/shadcn-io/aurora-background';
import RiskLensLogo from '@/assets/Risklens_Logo_Dark.png';

const AuthLayout = () => {
  return (
    <div className="flex items-center justify-center grow w-full h-screen p-2">
      <Outlet />
      <section className="w-2/5 h-full overflow-hidden rounded-lg dark">
        <AuroraBackground>
          <div className="relative z-10 px-16 flex flex-col gap-6">
            <img src={RiskLensLogo} alt="RiskLens" className="w-14" />
            <h1 className="text-4xl font-bold text-primary">
              Welcome to RiskLens
            </h1>
            <p className="text-muted-foreground w-2/3">
              We turn risk management into a measurable, repeatable discipline.
              Our platform quantifies exposure, prioritizes what matters most,
              and tracks mitigation performance over time.
            </p>
            <h2 className="text-lg text-muted-foreground">
              {' '}
              <span className="text-primary">17,000+ organizations</span> trust
              RiskLens.
              <br /> Now its your turn.
            </h2>
            <div className="border-1 border-zinc-600 rounded-lg p-4 backdrop-blur-xl bg-zinc-300/10 mt-12">
              <p className="text-muted-foreground text-sm">
                ©2025 Risk Exposure Tracker. All rights reserved.
                <br />
                For inquiries, please contact{' '}
                <span className="text-primary">
                  info@riskexposuretracker.com
                </span>
                .
              </p>
            </div>
          </div>
        </AuroraBackground>
      </section>
    </div>
  );
};

export default AuthLayout;
