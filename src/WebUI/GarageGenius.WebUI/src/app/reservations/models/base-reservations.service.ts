import { Observable } from 'rxjs';
import { VehicleReservationsResponseModel } from './vehicle-reservations-response.model';
import { VehicleReservationHistoryModel } from './vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from './vehicle-reservation-response.model';

export abstract class BaseReservationsService implements IReservationsService {
  abstract getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  abstract getReservationById(
    reservationId: string
  ): Observable<VehicleReservationResponseModel>;
  abstract getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
}

export interface IReservationsService {
  getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  getReservationById(
    reservationId: string
  ): Observable<VehicleReservationResponseModel>;
  getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
}
