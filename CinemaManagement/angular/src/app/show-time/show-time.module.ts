import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShowTimeRoutingModule } from './show-time-routing.module';
import { ShowTimeComponent } from './show-time.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ShowTimeComponent
  ],
  imports: [
    CommonModule,
    ShowTimeRoutingModule,
    SharedModule
  ]
})
export class ShowTimeModule { }
