import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from "../authentication/service/authentication.service";

@Injectable()
export class JsonWebTokenInterceptor implements HttpInterceptor {

  constructor(private _authenticationSerivce: AuthenticationService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const jsonWebToken: string = this._authenticationSerivce.getAuthenticationToken();
    const isUserLogged: boolean = jsonWebToken !== null;

    if (isUserLogged) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jsonWebToken}`
        }
        // TODO - implement cookie storage?
      });
    }

    return next.handle(request);
  }
}

export const jwtInterceptorProvider = [
{ provide: JsonWebTokenInterceptor, useClass: JsonWebTokenInterceptor, multi: true },
]
