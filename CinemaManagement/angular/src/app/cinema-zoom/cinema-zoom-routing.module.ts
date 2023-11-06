import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CinemaZoomComponent } from './cinema-zoom.component';

const routes: Routes = [{ path: '', component: CinemaZoomComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CinemaZoomRoutingModule { }
