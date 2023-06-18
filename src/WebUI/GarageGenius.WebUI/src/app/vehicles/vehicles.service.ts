import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class VehiclesService {
  private _httpClient: HttpClient;
  private customerId?: string;
  private _createVehicle: string =
    environment.vehiclesApiUrl +
    'vehicles/' +
    `vehicles/customers/${this.customerId}/vehicle`;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }
}
