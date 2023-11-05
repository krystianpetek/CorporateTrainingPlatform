import { Observable } from 'rxjs';
import {GetUsersResponseModel} from "./get-users-response-model";

export abstract class BaseUserService implements IUserService {
  abstract getUsers(): Observable<GetUsersResponseModel>;
}

export interface IUserService {
  getUsers(): Observable<GetUsersResponseModel>;
}
