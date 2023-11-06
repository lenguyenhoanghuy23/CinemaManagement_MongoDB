import type { ticketDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class tickerService {
  apiName = 'Default';
  

  create = (showTimeCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ticketDto>({
      method: 'POST',
      url: '/api/app/ticker',
      params: { showTimeCode },
    },
    { apiName: this.apiName,...config });
  

  delete = (showTimeCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/ticker',
      params: { showTimeCode },
    },
    { apiName: this.apiName,...config });
  

  get = (showtimeCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ticketDto>>({
      method: 'GET',
      url: '/api/app/ticker',
      params: { showtimeCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ticketDto>>({
      method: 'GET',
      url: '/api/app/ticker/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
