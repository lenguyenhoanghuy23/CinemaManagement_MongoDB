import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BackupRoutingModule } from './backup-routing.module';
import { BackupComponent } from './backup.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    BackupComponent
  ],
  imports: [
    CommonModule,
    BackupRoutingModule,
    SharedModule
  ]
})
export class BackupModule { }
