import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzTreeViewModule } from 'ng-zorro-antd/tree-view';
import { NzTableModule } from 'ng-zorro-antd/table';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzResultModule } from 'ng-zorro-antd/result';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { registerLocaleData } from '@angular/common';
import { NzTransferModule } from 'ng-zorro-antd/transfer';
import { NzSpinModule } from 'ng-zorro-antd/spin';

import vi from '@angular/common/locales/vi';
registerLocaleData(vi);

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NzModalModule,
    NzButtonModule,
    NgbPaginationModule,
    NzSelectModule,
    NzDatePickerModule,
    NzTagModule,
    NzListModule,
    NzTreeViewModule,
    NzTableModule,
    ScrollingModule,
    NzGridModule,
    NzImageModule,
    NzResultModule,
    NzNotificationModule,
    NzTransferModule,
    NzSpinModule
  ],
  providers: []
})
export class SharedModule {}
