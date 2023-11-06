import type { showTimeCreateDto, showTimeDto, showTimeUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class showTimeService {
  apiName = 'Default';
  

  create = (input: showTimeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, showTimeDto>({
      method: 'POST',
      url: '/api/app/show-time',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (showtimeCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/show-time',
      params: { showtimeCode },
    },
    { apiName: this.apiName,...config });
  

  get = (showtimeCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, showTimeDto>({
      method: 'GET',
      url: '/api/app/show-time',
      params: { showtimeCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<showTimeDto>>({
      method: 'GET',
      url: '/api/app/show-time/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (showtimeCode: string, input: showTimeUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, showTimeDto>({
      method: 'PUT',
      url: '/api/app/show-time',
      params: { showtimeCode },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
