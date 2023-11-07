import {inject} from "@angular/core";
import {CanActivateFn, Router} from '@angular/router';
import {AuthenticationService} from "../services/authentication/authentication.service";
import {SnackBarMessageService} from "../services/snack-bar-message/snack-bar-message.service";
import {Role} from "../services/authentication/models/role.model";

export const staffGuard: CanActivateFn = (route, state) => {
  const authenticationService: AuthenticationService = inject(AuthenticationService);
  const router: Router = inject(Router);
  const snackBarMessageService: SnackBarMessageService = inject(SnackBarMessageService);

  const fail = (): boolean => {
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

  const user = authenticationService.getUserInfo();
  if (!user) {
    fail();
    return false;
  }
  const userRole = JSON.parse(JSON.stringify(user)).role;

  if(user.role !== Role.Customer)
    return true;

  fail();
  return false;
};
