import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowTimeComponent } from './show-time.component';

const routes: Routes = [{ path: '', component: ShowTimeComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShowTimeRoutingModule { }
