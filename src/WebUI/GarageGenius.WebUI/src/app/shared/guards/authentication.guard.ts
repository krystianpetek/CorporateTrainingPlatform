import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { SnackBarMessageService } from '../services/snack-bar-message/snack-bar-message.service';
import { inject } from '@angular/core';

export const authenticationGuard: CanActivateFn = (route, state) => {
  const authenticationService: AuthenticationService = inject(
    AuthenticationService
  );
  const router: Router = inject(Router);
  const snackBarMessageService: SnackBarMessageService = inject(
    SnackBarMessageService
  );

  const user = authenticationService.getUserInfo();
  if (user) {
    return true;
  }

  snackBarMessageService.fail(
    'You are not authorized to access this page, please log in',
    5
  );
  router.navigate(['authentication/sign-in'], {
    replaceUrl: true,
    queryParams: { returnUrl: state.url },
  });
  return false;
};

//if (authenticationService.getAuthenticationToken() !== null)
//  return true;

//return router.parseUrl(`/`);
