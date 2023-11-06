import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { showTimeDto } from '../show-times/models';
import type { ticketUpdateDto } from '../tickets/models';

@Injectable({
  providedIn: 'root',
})
export class SellerService {
  apiName = 'Default';
  

  getlistScheduler = (schedule: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<showTimeDto>>({
      method: 'GET',
      url: '/api/app/seller/list-scheduler',
      params: { schedule },
    },
    { apiName: this.apiName,...config });
  

  update = (input: ticketUpdateDto[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/app/seller/update',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
