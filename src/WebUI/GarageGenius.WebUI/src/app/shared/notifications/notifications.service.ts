import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {Notification} from "./notifications/models/notification.model";
import {VehicleReservationsResponseModel} from "../../reservations/models/vehicle-reservations-response.model";
import {environment} from "../../../environments/environment";



@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private httpClient: HttpClient) { }

  getNotifications(): Observable<Notification[]> {
    return this.httpClient
      .get<Notification[]>(
        environment.getNotificationsUrl
      )
      .pipe(catchError(this.handleError));
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
