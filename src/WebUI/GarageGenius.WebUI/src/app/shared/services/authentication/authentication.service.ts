import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SignUpModel } from './models/sign-up.model';
import { SignInModel } from './models/sign-in.model';
import { AuthenticationResponseModel } from './models/authentication-response.model';
import {
  IStorageService,
  BaseStorageService,
} from '../storage/models/base-storage.service';
import { StorageService } from '../storage/storage.service';

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
  getUserInfo(): AuthenticationResponseModel;
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
  abstract getUserInfo(): AuthenticationResponseModel;
}

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService extends AuthenticationServiceBase {
  private _httpClient: HttpClient;
  private _signUpPath: string = environment.signUpUrl;
  private _signInPath: string = environment.signInUrl;
  private _signOutPath: string = environment.signOutUrl;

  private _storageService: IStorageService;
  constructor(httpClient: HttpClient, storageService: StorageService) {
    super();
    this._httpClient = httpClient;
    this._storageService = storageService;
  }

  public override signUpUser(signUpModel: SignUpModel): Observable<void> {
    return this._httpClient.post<void>(
      document.baseURI + this._signUpPath,
      signUpModel
    );
  }

  public override signInUser(
    signInModel: SignInModel
  ): Observable<AuthenticationResponseModel> {
    return this._httpClient.post<AuthenticationResponseModel>(
      document.baseURI + this._signInPath,
      signInModel
    );
  }

  public override signOutUser(): Observable<void> {
    this._storageService.deleteKey(BaseStorageService.JWT_KEY);
    this._storageService.deleteKey(BaseStorageService.USER_KEY);
    return this._httpClient.post<void>(
      document.baseURI + this._signOutPath,
      {}
    );
  }

  public override setAuthenticationToken(jwt: string): void {
    this._storageService.setKey<string>(BaseStorageService.JWT_KEY, jwt);
  }

  public override getAuthenticationToken(): string {
    // TODO - change store jwt into cookie instead localStorage?
    return this._storageService.getKey<string>(BaseStorageService.JWT_KEY);
  }

  public override showMe(): Observable<unknown> {
    return this._httpClient.get<AuthenticationResponseModel>(
      document.baseURI + `users-module/users/me`
    );
  }

  override setUserInfo(userInfo: unknown): void {
    this._storageService.setKey<unknown>(BaseStorageService.USER_KEY, userInfo);
  }
  override getUserInfo(): AuthenticationResponseModel {
    return this._storageService.getKey<AuthenticationResponseModel>(
      BaseStorageService.USER_KEY
    );
  }
}
