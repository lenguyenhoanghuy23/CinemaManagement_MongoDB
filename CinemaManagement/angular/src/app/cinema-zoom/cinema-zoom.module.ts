import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CinemaZoomRoutingModule } from './cinema-zoom-routing.module';
import { CinemaZoomComponent } from './cinema-zoom.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    CinemaZoomComponent
  ],
  imports: [
    CommonModule,
    CinemaZoomRoutingModule,
    SharedModule
  ]
})
export class CinemaZoomModule { }
