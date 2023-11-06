import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SellerRoutingModule } from './seller-routing.module';
import { SellerComponent } from './seller.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    SellerComponent
  ],
  imports: [
    CommonModule,
    SellerRoutingModule,
    SharedModule
  ]
})
export class SellerModule { }
