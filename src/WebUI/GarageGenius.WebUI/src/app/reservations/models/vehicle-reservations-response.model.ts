export interface VehicleReservationsResponseModel {
  vehicleId: string;
  vehicleReservationsDto: Array<VehicleReservationsDto>;
}

export interface VehicleReservationsDto {
  reservationId: string;
  reservationState: string;
  reservationDate: string;
  comment: string;
}
