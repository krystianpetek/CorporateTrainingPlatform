import { Observable } from 'rxjs';
import { VehicleReservationsResponseModel } from './vehicle-reservations-response.model';
import { VehicleReservationHistoryModel } from './vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from './vehicle-reservation-response.model';
import { CustomerReservationsResponseModel } from './customer-reservations-response.model';

export abstract class BaseReservationService implements IReservationService {
  abstract getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  abstract getReservationById(
    reservationId: string
  ): Observable<VehicleReservationResponseModel>;
  abstract getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
  abstract getCustomerReservations(
    customerId: string
  ): Observable<CustomerReservationsResponseModel>;
}

export interface IReservationService {
  getVehicleReservations(
    vehicleId: string
  ): Observable<VehicleReservationsResponseModel>;
  getReservationById(
    reservationId: string
  ): Observable<VehicleReservationResponseModel>;
  getReservationHistory(
    reservationId: string
  ): Observable<VehicleReservationHistoryModel>;
  getCustomerReservations(
    customerId: string
  ): Observable<CustomerReservationsResponseModel>;
}
