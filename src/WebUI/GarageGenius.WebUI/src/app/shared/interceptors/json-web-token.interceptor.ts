import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable()
export class JsonWebTokenInterceptor implements HttpInterceptor {
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

    return next.handle(request).pipe(tap((value) => console.log(value)));
  }
}

export const jwtInterceptorProvider = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: JsonWebTokenInterceptor,
    deps: [AuthenticationService],
    multi: true,
  },
];
