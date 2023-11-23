export interface VehicleReservationResponseModel {
  reservationId: string;
  reservationState: string;
  reservationDate: Date;
  comment: string;
  vehicleId: string;
}

export interface ReservationAddRequestModel{
  vehicleId: string,
  customerId: string,
  reservationNote: string,
}
