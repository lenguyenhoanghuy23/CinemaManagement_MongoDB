import type { movieDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class movieService {
  apiName = 'Default';
  

  create = (objFile: File, datajson: string, config?: Partial<Rest.Config>): Observable<any>  =>{

    const formData = new FormData();
    formData.append('datajson', JSON.stringify(datajson));
    formData.append('objFile', objFile);
    return this.restService.request<any, movieDto>({
      method: 'POST',
      url: '/api/app/movie',
      params: { datajson },
      body: formData,
    },
    { apiName: this.apiName,...config });
  }


  delete = (movieCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/movie',
      params: { movieCode },
    },
    { apiName: this.apiName,...config });
  

  get = (movieCode: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, movieDto>({
      method: 'GET',
      url: '/api/app/movie',
      params: { movieCode },
    },
    { apiName: this.apiName,...config });
  

  getlist = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<movieDto>>({
      method: 'GET',
      url: '/api/app/movie/list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (movieCode: string, objFile: File, datajson: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, movieDto>({
      method: 'PUT',
      url: '/api/app/movie',
      params: { movieCode, datajson },
      body: objFile,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
