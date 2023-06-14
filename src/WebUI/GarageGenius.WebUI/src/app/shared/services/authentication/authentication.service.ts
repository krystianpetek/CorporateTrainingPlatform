import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SignUpModel } from './models/sign-up.model';
import { SignInModel } from './models/sign-in.model';
import { AuthenticationResponseModel } from './models/authentication-response.model';
import {
  IStorageService,
  StorageService,
} from 'src/app/storage/storage.service';

/**
 *  Interface for authentication service base
 */
export interface IAuthenticationService {
  signUpUser(signUpModel: SignUpModel): Observable<void>;
  signInUser(signInModel: SignInModel): Observable<AuthenticationResponseModel>;
  signOutUser(): Observable<void>;
  showMe(): Observable<unknown>;
  setAuthenticationToken(jwt: string): void;
  getAuthenticationToken(): string;
  setUserInfo(userInfo: unknown): void;
  getUserInfo(): unknown;
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
  abstract setAuthenticationToken(jwt: string): void;
  abstract getAuthenticationToken(): string;
  // TODO - change to user service
  abstract setUserInfo(userInfo: unknown): void;
  abstract getUserInfo(): unknown;
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

  private _storageService: IStorageService;

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
    this._storageService.cleanStorage();
    return this._httpClient.post<void>(this._signOutPath, {}, this.httpOptions);
  }

  public override setAuthenticationToken(jwt: string): void {
    this._storageService.setKey<string>(StorageService.JWT_KEY, jwt);
  }

  public override getAuthenticationToken(): string {
    // TODO - change store jwt into cookie instead localStorage?
    return this._storageService.getKey<string>(StorageService.JWT_KEY);
  }

  public override showMe(): Observable<unknown> {
    return this._httpClient.get<AuthenticationResponseModel>(
      this.basePath + `users-module/users/me`
    );
  }

  override setUserInfo(userInfo: unknown): void {
    this._storageService.setKey<unknown>(StorageService.USER_KEY, userInfo);
  }
  override getUserInfo(): unknown {
    return this._storageService.getKey<string>(StorageService.USER_KEY);
  }
}
