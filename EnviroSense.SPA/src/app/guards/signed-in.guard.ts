import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SessionService } from '../modules/api';

export const signedInGuard: CanActivateFn = (route, state) => {
  const sessionService = inject(SessionService);
  const router = inject(Router);

  if (!sessionService.isSignedIn()) {
    router.navigateByUrl('/guest/sign-in');
    return false;
  }

  return true;
};
