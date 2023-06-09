import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SignUpModel } from '../sign-up/models/sign-up.model';
import { SignInModel } from '../sign-in/models/sign-in.model';
import { AuthenticationResponseModel } from '../sign-in/models/authentication-response.model';
import {
  StorageService,
  StorageServiceBase,
} from 'src/app/storage/storage.service';

/**
 *  Interface for authentication service base
 */
export interface IAuthenticationService {
  signUpUser(signUpModel: SignUpModel): Observable<void>;
  signInUser(signInModel: SignInModel): Observable<AuthenticationResponseModel>;
  signOutUser(): Observable<void>;
  showMe(): Observable<unknown>;
  setAuthenticationToken(token: string): void;
  getAuthenticationToken(): string;
}
/**
 *  Abstract class for authentication service
 */
@Injectable({ providedIn: 'root' })
export abstract class AuthenticationServiceBase
  implements IAuthenticationService
{
  abstract signUpUser(signUpModel: SignUpModel): Observable<void>;
  abstract signInUser(
    signInModel: SignInModel
  ): Observable<AuthenticationResponseModel>;
  abstract signOutUser(): Observable<void>;
  abstract showMe(): Observable<unknown>;
  abstract setAuthenticationToken(token: string): void;
  abstract getAuthenticationToken(): string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService extends AuthenticationServiceBase {
  private _httpClient: HttpClient;
  private basePath: string = environment.baseUrl;
  private _signUpPath: string = environment.signUpUrl;
  private _signInPath: string = environment.signInUrl;
  private _signOutPath: string = environment.signOutUrl;

  private _storageService: StorageServiceBase;

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(httpClient: HttpClient, storageService: StorageService) {
    super();
    this._httpClient = httpClient;
    this._storageService = storageService;
  }

  public override signUpUser(signUpModel: SignUpModel): Observable<void> {
    return this._httpClient.post<void>(
      this.basePath + this._signUpPath,
      signUpModel,
      this.httpOptions
    );
  }

  public override signInUser(
    signInModel: SignInModel
  ): Observable<AuthenticationResponseModel> {
    return this._httpClient.post<AuthenticationResponseModel>(
      this.basePath + this._signInPath,
      signInModel,
      this.httpOptions
    );
  }

  public override signOutUser(): Observable<void> {
    return this._httpClient.post<void>(this._signOutPath, {}, this.httpOptions);
  }

  public override setAuthenticationToken(accessToken: string): void {
    localStorage.setItem('access-token', accessToken);
  }

  public override getAuthenticationToken(): string {
    // TODO - change store jwt into cookie instead localStorage?

    return localStorage.getItem('access-token') as string;
  }

  public override showMe(): Observable<unknown> {
    return this._httpClient.get<AuthenticationResponseModel>(
      this.basePath + `users-module/users/me`
    );
  }
}
