import type { countrieCreateDto, countrieDto, countrieUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CountrieService {
  apiName = 'Default';
  

  create = (input: countrieCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, countrieDto>({
      method: 'POST',
      url: '/api/app/countrie',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (countrieCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/countrie',
      params: { countrieCode },
    },
    { apiName: this.apiName,...config });
  

  get = (countrieCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, countrieDto>({
      method: 'GET',
      url: '/api/app/countrie',
      params: { countrieCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<countrieDto>>({
      method: 'GET',
      url: '/api/app/countrie/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (countrieCode: string, input: countrieUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, countrieDto>({
      method: 'PUT',
      url: '/api/app/countrie',
      params: { countrieCode },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
