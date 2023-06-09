import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SignUpModel } from '../sign-up/models/sign-up.model';
import { SignInModel } from '../sign-in/models/sign-in.model';
import { AuthenticationResponseModel } from '../sign-in/models/authentication-response.model';

interface IAuthenticationService {
  signUpUser(signUpModel: SignUpModel): Observable<void>;
  signInUser(signInModel: SignInModel): Observable<AuthenticationResponseModel>;
  showMe(): Observable<unknown>;
  setAuthenticationToken(token: string): void;
  getAuthenticationToken(): string;
}

@Injectable({ providedIn: 'root' })
export abstract class AuthenticationServiceBase
  implements IAuthenticationService
{
  abstract signUpUser(signUpModel: SignUpModel): Observable<void>;
  abstract signInUser(
    signInModel: SignInModel
  ): Observable<AuthenticationResponseModel>;
  abstract showMe(): Observable<unknown>;
  abstract setAuthenticationToken(token: string): void;
  abstract getAuthenticationToken(): string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService implements AuthenticationServiceBase {
  private _httpClient: HttpClient;
  private _signUpPath: string = environment.signUpUrl;
  private _signInPath: string = environment.signInUrl;
  private _signOutPath: string = environment.signOutUrl;
  private basePath: string = environment.baseUrl;

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public signUpUser(signUpModel: SignUpModel): Observable<void> {
    return this._httpClient.post<void>(
      this.basePath + this._signUpPath,
      signUpModel,
      this.httpOptions
    );
  }

  public signInUser(
    signInModel: SignInModel
  ): Observable<AuthenticationResponseModel> {
    return this._httpClient.post<AuthenticationResponseModel>(
      this.basePath + this._signInPath,
      signInModel,
      this.httpOptions
    );
  }

  public signOutUser(): Observable<void> {
    return this._httpClient.post<void>(this._signOutPath, {}, this.httpOptions);
  }

  public setAuthenticationToken(accessToken: string): void {
    localStorage.setItem('access-token', accessToken);
  }

  public getAuthenticationToken(): string {
    // TODO - change store jwt into cookie instead localStorage?
    return localStorage.getItem('access-token') as string;
  }

  public showMe(): Observable<unknown> {
    return this._httpClient.get<AuthenticationResponseModel>(
      this.basePath + `users-module/users/me`
    );
  }
}
