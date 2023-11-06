import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'CinemaManagement',
    logoUrl: '../assets/images/logo/logCinema.jpg',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44333/',
    redirectUri: baseUrl,
    clientId: 'CinemaManagement_App',
    responseType: 'code',
    scope: 'offline_access CinemaManagement',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44333',
      rootNamespace: 'CinemaManagement',
    },
  },
} as Environment;
