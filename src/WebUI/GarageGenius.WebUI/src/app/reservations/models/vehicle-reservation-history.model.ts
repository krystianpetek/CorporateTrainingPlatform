export interface VehicleReservationHistoryModel {
  reservationId: string;
  reservationHistoriesDtos: ReservationHistoryResponseModel;
}

export interface ReservationHistoryResponseModel {
  reservationHistoryId: string;
  reservationState: string;
  comment: string;
}
