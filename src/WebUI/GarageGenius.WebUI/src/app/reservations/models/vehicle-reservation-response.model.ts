export interface VehicleReservationResponseModel {
  reservationId: string;
  reservationState: string;
  comment: string;
}

export interface ReservationAddRequestModel{
  vehicleId: string,
  customerId: string,
  reservationNote: string,
}
