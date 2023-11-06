import type { cinemaZoomDto, cinemaZoomUpdateDto, cinemaZoonCreateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CinemaZoomService {
  apiName = 'Default';
  

  create = (input: cinemaZoonCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, cinemaZoomDto>({
      method: 'POST',
      url: '/api/app/cinema-zoom',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (cinemaCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/cinema-zoom',
      params: { cinemaCode },
    },
    { apiName: this.apiName,...config });
  

  get = (cinemaCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, cinemaZoomDto>({
      method: 'GET',
      url: '/api/app/cinema-zoom',
      params: { cinemaCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<cinemaZoomDto>>({
      method: 'GET',
      url: '/api/app/cinema-zoom/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (cinemaCode: string, input: cinemaZoomUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, cinemaZoomDto>({
      method: 'PUT',
      url: '/api/app/cinema-zoom',
      params: { cinemaCode },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
