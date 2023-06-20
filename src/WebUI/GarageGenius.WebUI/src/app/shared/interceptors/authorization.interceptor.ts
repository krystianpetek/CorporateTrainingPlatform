import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS,
  HttpErrorResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { EMPTY, Observable, catchError, throwError } from 'rxjs';
import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  private readonly _authenticationSerivce: AuthenticationService;

  constructor(authenticationSerivce: AuthenticationService) {
    this._authenticationSerivce = authenticationSerivce;
  }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const jsonWebToken: string =
      this._authenticationSerivce.getAuthenticationToken();
    const isUserLogged: boolean = jsonWebToken !== null;

    // TODO - check whether this request url is the same
    if (isUserLogged) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jsonWebToken}`,
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        // TODO - implement cookie storage?
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === HttpStatusCode.Unauthorized) {
          this._authenticationSerivce.signOutUser();
          return EMPTY;
        } else {
          return throwError(() => error);
        }
      })
    );
  }
}

export const jwtInterceptorProvider = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthorizationInterceptor,
    deps: [AuthenticationService],
    multi: true,
  },
];
