import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GenreRoutingModule } from './genre-routing.module';
import { GenreComponent } from './genre.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    GenreComponent
  ],
  imports: [
    CommonModule,
    GenreRoutingModule,
    SharedModule
  ]
})
export class GenreModule { }
