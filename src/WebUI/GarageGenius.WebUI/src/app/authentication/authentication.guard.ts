import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from './service/authentication.service';
import { inject } from '@angular/core';

export const authenticationGuard: CanActivateFn = (route, state) => {
  const authenticationService: AuthenticationService = inject(AuthenticationService);
  const router: Router = inject(Router);

  if (authenticationService.getAuthenticationToken() !== null)
    return true;

  return router.parseUrl(`/`);
};
