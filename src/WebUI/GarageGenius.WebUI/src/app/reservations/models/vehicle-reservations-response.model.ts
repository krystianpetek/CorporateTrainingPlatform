export interface VehicleReservationsResponseModel {
  vehicleId: string;
  customerReservationsDto: Array<VehicleReservationsDto>;
}

export interface VehicleReservationsDto {
  reservationId: string;
  reservationState: string;
  reservationDate: string;
  comment: string;
}
