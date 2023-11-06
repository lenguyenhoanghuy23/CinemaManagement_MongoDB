
export interface ticketDto {
  id?: string;
  typeticker: number;
  seatcode?: string;
  status: number;
  priceticker: number;
  price: number;
  showtime?: string;
}

export interface ticketUpdateDto {
  showtime?: string;
  seatsCode?: string;
  priceticker: number;
}
