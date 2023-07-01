import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseReservationService } from '../models/base-reservation.service';
import { Observable, catchError, throwError } from 'rxjs';
import { VehicleReservationsResponseModel } from '../models/vehicle-reservations-response.model';
import { environment } from 'src/environments/environment';
import { VehicleReservationHistoryModel } from '../models/vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from '../models/vehicle-reservation-response.model';
import { CustomerReservationsResponseModel } from '../models/customer-reservations-response.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService extends BaseReservationService {
  private _httpClient: HttpClient;
  constructor(httpClient: HttpClient) {
    super();
    this._httpClient = httpClient;
  }

  override getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel> {
    return this._httpClient
      .get<VehicleReservationsResponseModel>(
        environment.reservationsApiUrl +
          `reservations/vehicle/${vehicleId}/reservations`
      )
      .pipe(catchError(this.handleError));
  }

  override getReservationById(
    reservationId: string
  ): Observable<VehicleReservationResponseModel> {
    return this._httpClient
      .get<VehicleReservationResponseModel>(
        environment.reservationsApiUrl + `reservations/${reservationId}`
      )
      .pipe(catchError(this.handleError));
  }

  override getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel> {
    return this._httpClient
      .get<VehicleReservationHistoryModel>(
        environment.reservationsApiUrl + `reservations/${reservationId}/history`
      )
      .pipe(catchError(this.handleError));
  }

  override getCustomerReservations(
    customerId: string
  ): Observable<CustomerReservationsResponseModel> {
    return this._httpClient
      .get<CustomerReservationsResponseModel>(
        environment.reservationsApiUrl + `reservations/customer`,
        {
          params: {
            customerId: customerId,
          },
        }
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
