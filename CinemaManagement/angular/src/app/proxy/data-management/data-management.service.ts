import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataManagementService {
  apiName = 'Default';
  

  backup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/data-management/backup',
    },
    { apiName: this.apiName,...config });
  

  exportToJsonByCollectionNames = (CollectionNames: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/data-management/export-to-json',
      body: CollectionNames,
    },
    { apiName: this.apiName,...config });
  

  showCollection = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string[]>({
      method: 'POST',
      url: '/api/app/data-management/show-collection',
    },
    { apiName: this.apiName,...config });
  

  reStoreByBackupDirectoryAndDbName = (backupDirectory: string, dbName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/data-management/re-store',
      params: { backupDirectory, dbName },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
