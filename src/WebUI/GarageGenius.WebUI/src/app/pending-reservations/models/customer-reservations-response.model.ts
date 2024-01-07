export interface CurrentNotCompletedReservationsResponseModel {
  currentNotCompletedReservationsDtos: CurrentNotCompletedReservationsDtos;
}

export interface CurrentNotCompletedReservationsDtos {
  currentPage: number;
  resultsPerPage: number;
  totalPages: number;
  totalResults: number;
  items: Array<CustomerReservationsItem>;
}
export interface CustomerReservationsItem {
  reservationId: string;
  vehicleId: string;
  customerId: string;
  reservationState: string;
  reservationDate: string;
  comment: string;
}
