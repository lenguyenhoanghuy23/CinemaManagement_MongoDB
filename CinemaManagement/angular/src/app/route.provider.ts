import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      
      },
      {
        path: '/screenTypes',
        name: '::screenTypes',
        iconClass: 'fas fa-tv',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.screenTypes',
      },
      {
        path: '/CinemaZooms',
        name: '::cinemaRooms',  
        iconClass: 'fas fa-video ',
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.CinemaZooms',
      },
      {
        path: '/Countries',
        name: '::countries',
        iconClass: 'fas fa-map-marked ',  
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.Countries',
      },
      {
        path: '/Genres',
        name: '::genres',
        iconClass: 'fas fa-tags',
        order: 5,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.Genres',
      },
      {
        path: '/Movies',
        name: '::movies',
        iconClass: 'fas fa-home',
        order: 6,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.Movies',
      },
      {
        path: '/showTimes',
        name: '::showTimes',
        iconClass: 'fas fa-calendar-alt',
        order: 7,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.ShowTimes',
      },
      {
        path: '/Tickets',
        name: '::tickets',
        iconClass: 'fas fa-ticket-alt',
        order: 8,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.Tickets',
      },
      {
        path: '/Sellers',
        name: '::sellers',
        iconClass: 'fas fa-shopping-cart',
        order: 9,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.Sellers',
      },
      {
        path: '/revenues',
        name: '::revenues',
        iconClass: "fas fa-chart-line",
        order: 10,
        layout: eLayoutType.application,

      },
      {
        path: '/DataManagement',
        name: '::Data Management',
        iconClass: "fas fa-tasks",
        order: 11,
        layout: eLayoutType.application,
        requiredPolicy: 'CinemaManagement.DataManagement',
      },

      {
        path: '/Backups',
        name: '::backups&restore',
        parentName:"::Data Management",
        iconClass: "fas fa-hdd",
        layout: eLayoutType.application,
      },
      {
        path: '/Exports',
        name: '::exports',
        parentName:"::Data Management",
        iconClass: "fas fa-file-export",
        layout: eLayoutType.application,
      },
    ]);
  };
}
