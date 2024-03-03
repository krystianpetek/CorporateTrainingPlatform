import { Observable } from 'rxjs';
import {GetUserResponseModel, GetUsersResponseModel} from "./get-users-response-model";
import {UserRequestModel, UserResponseModel} from "./user.model";

export abstract class BaseUserService implements IUserService {
  abstract getUsers(): Observable<GetUsersResponseModel>;
  abstract getUserById(userId: string): Observable<GetUserResponseModel>;
  abstract getLoggedUser(): Observable<any>; // todo - define the type
  abstract postUser(user: UserRequestModel): Observable<UserResponseModel>;
}

export interface IUserService {
  getUsers(): Observable<GetUsersResponseModel>;
  getUserById(userId: string): Observable<GetUserResponseModel>;
  getLoggedUser(): Observable<any>; // todo - define the type
  postUser(user: UserRequestModel): Observable<UserResponseModel>;
}
