import type { genreCreateDto, genreDto, genreUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class genreService {
  apiName = 'Default';
  

  create = (input: genreCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, genreDto>({
      method: 'POST',
      url: '/api/app/genre',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (genreCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/genre',
      params: { genreCode },
    },
    { apiName: this.apiName,...config });
  

  get = (genreCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, genreDto>({
      method: 'GET',
      url: '/api/app/genre',
      params: { genreCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<genreDto>>({
      method: 'GET',
      url: '/api/app/genre/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (genreCode: string, input: genreUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, genreDto>({
      method: 'PUT',
      url: '/api/app/genre',
      params: { genreCode },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
