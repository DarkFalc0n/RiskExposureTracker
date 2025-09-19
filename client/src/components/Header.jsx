import React from 'react';
import { cn } from '@/lib/utils';

const Header = ({ className }) => {
  return (
    <div
      className={cn(
        className,
        'flex items-center justify-center h-20 bg-zinc-900 text-neutral-300 shrink-0'
      )}
    >
      Header
    </div>
  );
};

export default Header;
