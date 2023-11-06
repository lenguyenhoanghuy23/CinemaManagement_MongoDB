
export interface movieDto {
  id?: string;
  movieCode?: string;
  movieName?: string;
  description?: string;
  runningTime: number;
  releaseDate?: string;
  endDate?: string;
  year: number;
  producer?: string;
  actor?: string;
  director?: string;
  genres: string[];
  genreName: string[];
  countries: string[];
  countrieName: string[];
  avata?: string;
}
