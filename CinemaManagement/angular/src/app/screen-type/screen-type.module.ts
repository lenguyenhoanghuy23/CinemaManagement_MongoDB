import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ScreenTypeRoutingModule } from './screen-type-routing.module';
import { ScreenTypeComponent } from './screen-type.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ScreenTypeComponent
  ],
  imports: [
    CommonModule,
    ScreenTypeRoutingModule,
    SharedModule
  ]
})
export class ScreenTypeModule { }
