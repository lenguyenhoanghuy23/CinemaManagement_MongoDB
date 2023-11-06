import type { screenTypeCreateDto, screenTypeDto, screenTypeUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class screenTypeService {
  apiName = 'Default';
  

  create = (input: screenTypeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, screenTypeDto>({
      method: 'POST',
      url: '/api/app/screen-type',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (screenCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/screen-type',
      params: { screenCode },
    },
    { apiName: this.apiName,...config });
  

  get = (screenCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, screenTypeDto>({
      method: 'GET',
      url: '/api/app/screen-type',
      params: { screenCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<screenTypeDto>>({
      method: 'GET',
      url: '/api/app/screen-type/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (screenCode: string, input: screenTypeUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, screenTypeDto>({
      method: 'PUT',
      url: '/api/app/screen-type',
      params: { screenCode },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
