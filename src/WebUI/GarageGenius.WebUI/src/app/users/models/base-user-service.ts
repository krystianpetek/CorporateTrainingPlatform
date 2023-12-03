import { Observable } from 'rxjs';
import {GetUsersResponseModel} from "./get-users-response-model";
import {UserRequestModel, UserResponseModel} from "./user.model";
import {VehicleRequestModel, VehicleResponseModel} from "../../vehicles/models/vehicle.model";

export abstract class BaseUserService implements IUserService {
  abstract getUsers(): Observable<GetUsersResponseModel>;
  abstract postUser(user: UserRequestModel): Observable<UserResponseModel>;
}

export interface IUserService {
  getUsers(): Observable<GetUsersResponseModel>;
  postUser(user: UserRequestModel): Observable<UserResponseModel>;
}
