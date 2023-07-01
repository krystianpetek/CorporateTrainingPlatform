export interface CustomerReservationsResponseModel {
  customerId: string;
  customerReservationsDto: CustomerReservationsDto;
}

export interface CustomerReservationsDto {
  currentPage: number;
  resultsPerPage: number;
  totalPages: number;
  totalResults: number;
  items: Array<CustomerReservationsItem>;
}
export interface CustomerReservationsItem {
  reservationId: string;
  reservationState: string;
  reservationDate: string;
  comment: string;
}
