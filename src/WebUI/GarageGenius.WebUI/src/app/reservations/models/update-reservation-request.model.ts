export interface UpdateReservationRequestModel {
  reservationId: string;
  reservationState: string;
  reservationDate: Date;
  reservationNote: string;
  changerId: string;
}
