import { Observable } from 'rxjs';
import { VehicleReservationsResponseModel } from './vehicle-reservations-response.model';

export abstract class BaseReservationsService implements IReservationsService {
  abstract getVehicleReservations(
    vehicleId: string
  ): Observable<Array<VehicleReservationsResponseModel>>;
}

export interface IReservationsService {
  getVehicleReservations(
    vehicleId: string
  ): Observable<Array<VehicleReservationsResponseModel>>;
}
