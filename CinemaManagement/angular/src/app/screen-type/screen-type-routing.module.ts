import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ScreenTypeComponent } from './screen-type.component';

const routes: Routes = [{ path: '', component: ScreenTypeComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScreenTypeRoutingModule { }
