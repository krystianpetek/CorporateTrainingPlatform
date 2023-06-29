import { Observable } from 'rxjs';
import { VehicleReservationsResponseModel } from './vehicle-reservations-response.model';
import { VehicleReservationHistoryModel } from './vehicle-reservation-history.model';

export abstract class BaseReservationsService implements IReservationsService {
  abstract getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  abstract getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
}

export interface IReservationsService {
  getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
}
