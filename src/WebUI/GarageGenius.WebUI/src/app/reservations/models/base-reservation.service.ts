import { Observable } from 'rxjs';
import { VehicleReservationsResponseModel } from './vehicle-reservations-response.model';
import { VehicleReservationHistoryModel } from './vehicle-reservation-history.model';
import {ReservationAddRequestModel, VehicleReservationResponseModel} from './vehicle-reservation-response.model';
import { CustomerReservationsResponseModel } from './customer-reservations-response.model';
import {
  CurrentNotCompletedReservationsResponseModel
} from "../../pending-reservations/models/customer-reservations-response.model";
import {UpdateReservationRequestModel} from "./update-reservation-request.model";

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
  abstract addReservation(
    reservation: ReservationAddRequestModel
  ): Observable<void>;
  abstract getNotCompletedReservations(): Observable<CurrentNotCompletedReservationsResponseModel>;
  abstract updateReservation(reservation: UpdateReservationRequestModel): Observable<void>;
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
  addReservation(
    reservation: ReservationAddRequestModel
  ): Observable<void>;
  getNotCompletedReservations(): Observable<CurrentNotCompletedReservationsResponseModel>;
  updateReservation(reservation: UpdateReservationRequestModel): Observable<void>;
}
