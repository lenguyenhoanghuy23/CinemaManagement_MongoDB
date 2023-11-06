
export interface showTimeCreateDto {
  showTimeCode?: string;
  movieSchedule?: string;
  price: number;
  movie?: string;
  screenType?: string;
  cinemaZoom?: string;
}

export interface showTimeDto {
  showTimeCode?: string;
  movieSchedule?: string;
  price: number;
  status: number;
  movie?: string;
  movieAvata?: string;
  movieName?: string;
  screenType?: string;
  cinemaZoom?: string;
}

export interface showTimeUpdateDto {
  movieSchedule?: string;
  price: number;
  movie?: string;
  screenType?: string;
  cinemaZoom?: string;
}
