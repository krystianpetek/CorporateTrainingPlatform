export interface VehicleReservationHistoryModel {
  reservationId: string;
  reservationHistoriesDtos: Array<ReservationHistoryDto>;
}

export interface ReservationHistoryDto {
  reservationHistoryId: string;
  updateDate: Date;
  reservationState: string;
  comment: string;
  userId: string;
}
