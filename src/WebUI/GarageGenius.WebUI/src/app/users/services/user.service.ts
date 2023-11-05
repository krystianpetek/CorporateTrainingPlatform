import { Injectable } from '@angular/core';
import {BaseUserService} from "../models/base-user-service";
import {catchError, Observable, throwError} from "rxjs";
import {GetUsersResponseModel} from "../models/get-users-response-model";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseUserService {

  constructor(private _httpClient: HttpClient) {
    super();
  }

  override getUsers(): Observable<GetUsersResponseModel> {
    return this._httpClient
      .get<GetUsersResponseModel>(environment.getUsersUrl)
      .pipe(catchError(this.handleError))
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    // TODO - add a remote logging service like in backend - Serilog with Seq sink
    return throwError(() => errorMessage);
  }
}
