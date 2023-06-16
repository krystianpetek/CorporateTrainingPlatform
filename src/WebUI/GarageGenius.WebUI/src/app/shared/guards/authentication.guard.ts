import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { inject } from '@angular/core';

export const authenticationGuard: CanActivateFn = async (route, state) => {
  const authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  const router: Router = inject(Router);

  const user = authenticationService.getUserInfo();
  if (user) {
    return true;
  }

  await router.navigate(['authentication/sign-in'], {
    replaceUrl: true,
    queryParams: { returnUrl: state.url },
  });
  return false;
};

//if (authenticationService.getAuthenticationToken() !== null)
//  return true;

//return router.parseUrl(`/`);
