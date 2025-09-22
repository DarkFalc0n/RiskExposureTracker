import React from 'react';
import { cn } from '@/lib/utils';
const Footer = ({ className }) => {
  return (
    <div
      className={cn(
        className,
        'flex items-center justify-start h-20 bg-zinc-900 text-neutral-300'
      )}
    >
      ©2025 Risk Exposure Tracker
    </div>
  );
};

export default Footer;
