import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import {
  VehicleRequestModel,
  VehicleResponseModel,
} from '../models/vehicle.model';
import { BaseVehicleService } from '../models/base-vehicle.service';
import { SearchVehicleVinLicenseModel } from '../models/search-vehicle-vin-license.model';

@Injectable({
  providedIn: 'root',
})
export class VehiclesService extends BaseVehicleService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    super();
    this._httpClient = httpClient;
  }

  public override getCustomerVehicles(
    customerId: string
  ): Observable<Array<VehicleResponseModel>> {
    return this._httpClient
      .get<Array<VehicleResponseModel>>(
        environment.vehiclesApiUrl + `vehicles/${customerId}/vehicles`
      )
      .pipe(catchError(this.handleError));
  }

  public override getVehicleById(
    vehicleId: string
  ): Observable<VehicleResponseModel> {
    return this._httpClient
      .get<VehicleResponseModel>(
        environment.vehiclesApiUrl + `vehicles/${vehicleId}`
      )
      .pipe(catchError(this.handleError));
  }

  public override searchVehicleByVinAndLicensePlate(
    searchVehicle: SearchVehicleVinLicenseModel
  ): Observable<VehicleResponseModel> {
    return this._httpClient
      .get<VehicleResponseModel>(
        environment.vehiclesApiUrl + `vehicles/search`,
        {
          params: {
            vin: searchVehicle.vin,
            licensePlate: searchVehicle.licensePlate,
          },
        }
      )
      .pipe(catchError(this.handleError));
  }

  public override postVehicleForCustomer(
    customerId: string,
    vehicle: VehicleRequestModel
  ): Observable<VehicleResponseModel> {
    return this._httpClient
      .post<VehicleResponseModel>(
        environment.vehiclesApiUrl + `vehicles/customers/${customerId}/vehicle`,
        vehicle
      )
      .pipe(catchError(this.handleError));
  }

  public override updateVehicleCustomer(
    vehicleId: string,
    customerId: string
  ): Observable<void> {
    return this._httpClient
      .patch<void>(
        environment.vehiclesApiUrl + `vehicles/${vehicleId}/customer`,
        customerId
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
