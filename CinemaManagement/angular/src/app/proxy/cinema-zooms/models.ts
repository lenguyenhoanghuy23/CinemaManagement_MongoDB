import type { StatusEmu } from '../status/status-emu.enum';

export interface cinemaZoomDto {
  id?: string;
  cinemaCode?: string;
  cinemaName?: string;
  numberSeats: number;
  rowSeats: number;
  columnSeats: number;
  status: StatusEmu;
}

export interface cinemaZoomUpdateDto {
  cinemaName?: string;
  numberSeats: number;
  rowSeats: number;
  columnSeats: number;
  status: StatusEmu;
}

export interface cinemaZoonCreateDto {
  cinemaCode?: string;
  cinemaName?: string;
  numberSeats: number;
  rowSeats: number;
  columnSeats: number;
  status: StatusEmu;
}
