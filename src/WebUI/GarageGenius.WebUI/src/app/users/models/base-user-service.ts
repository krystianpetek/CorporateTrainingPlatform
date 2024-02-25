import { Observable } from 'rxjs';
import {GetUsersResponseModel} from "./get-users-response-model";
import {UserRequestModel, UserResponseModel} from "./user.model";

export abstract class BaseUserService implements IUserService {
  abstract getUsers(): Observable<GetUsersResponseModel>;
  abstract getLoggedUser(): Observable<any>; // todo - define the type
  abstract postUser(user: UserRequestModel): Observable<UserResponseModel>;
}

export interface IUserService {
  getUsers(): Observable<GetUsersResponseModel>;
  getLoggedUser(): Observable<any>; // todo - define the type
  postUser(user: UserRequestModel): Observable<UserResponseModel>;
}
