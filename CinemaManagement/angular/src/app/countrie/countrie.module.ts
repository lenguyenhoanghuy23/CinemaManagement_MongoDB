import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CountrieRoutingModule } from './countrie-routing.module';
import { CountrieComponent } from './countrie.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    CountrieComponent
  ],
  imports: [
    CommonModule,
    CountrieRoutingModule,
    SharedModule
  ]
})
export class CountrieModule { }
