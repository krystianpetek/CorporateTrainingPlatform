import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from 'src/environments/environment';
import {SignUpModel} from "./sign-up/models/sign-up.model";


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private _signUpPath: string = environment.signUpUrl;
  private _signInPath: string = environment.signInUrl;
  private basePath: string = environment.baseUrl;

  private constructor(private _httpClient: HttpClient) {
  }

  public signUp(payload: SignUpModel): Observable<SignUpModel> {
    return this._httpClient.post<SignUpModel>(this.basePath + this._signUpPath, payload);
  }
}
