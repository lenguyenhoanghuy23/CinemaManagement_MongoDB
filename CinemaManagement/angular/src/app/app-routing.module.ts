import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'screenTypes', loadChildren: () => import('./screen-type/screen-type.module').then(m => m.ScreenTypeModule) },
  { path: 'CinemaZooms', loadChildren: () => import('./cinema-zoom/cinema-zoom.module').then(m => m.CinemaZoomModule) },
  { path: 'Countries', loadChildren: () => import('./countrie/countrie.module').then(m => m.CountrieModule) },
  { path: 'Genres', loadChildren: () => import('./genre/genre.module').then(m => m.GenreModule) },
  { path: 'Movies', loadChildren: () => import('./movie/movie.module').then(m => m.MovieModule) },
  { path: 'showTimes', loadChildren: () => import('./show-time/show-time.module').then(m => m.ShowTimeModule) },
  { path: 'Tickets', loadChildren: () => import('./ticket/ticket.module').then(m => m.TicketModule) },
  { path: 'Sellers', loadChildren: () => import('./seller/seller.module').then(m => m.SellerModule) },
  { path: 'revenues', loadChildren: () => import('./revenue/revenue.module').then(m => m.RevenueModule) },
  { path: 'Backups', loadChildren: () => import('./backup/backup.module').then(m => m.BackupModule) },
  { path: 'Exports', loadChildren: () => import('./export/export.module').then(m => m.ExportModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
