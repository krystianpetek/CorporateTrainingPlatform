export interface VehicleReservationHistoryModel {
  reservationId: string;
  reservationHistoriesDtos: ReservationHistoryDto;
}

export interface ReservationHistoryDto {
  reservationHistoryId: string;
  reservationState: string;
  comment: string;
}
