import { Component, OnInit } from '@angular/core';
import { TransferChange, TransferItem } from 'ng-zorro-antd/transfer';
import { NzI18nService, en_US } from 'ng-zorro-antd/i18n';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { DataManagementService } from '@proxy/data-management';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.scss']
})
export class ExportComponent implements OnInit {
  list: TransferItem[] =[];

  data = ["ScreenTypes" ,"Genres","Countries","Movies","CinemaZooms","ShowTimes","Tickets"]

  selectedCollection :string[]

  constructor(
    public msg: NzMessageService,
    private dataManagementService : DataManagementService,
    private i18n: NzI18nService,
    private notification: NzNotificationService,
    private confirmation: ConfirmationService
  ){
    this.i18n.setLocale( en_US );
  }
  ngOnInit() {
    
    this.getData()
    
  }

  // getCollection (){
  //   const ret: TransferItem[] = [];
  //   this.exportService.showCollection().subscribe((data) =>  data.map((x) => this.selected.push(x)));

  //   console.log(this.selected);
    
    
  //   this.list = ret
    
  // }


  getData(): void {
    const ret: TransferItem[] = [];
    this.data.forEach((x ,i) =>{
      ret.push({
        key:i.toString(),
        title:x
      })
    })

    this.list = ret;
  }

  reload(direction: string): void {
    this.getData();
    this.msg.success(`your clicked ${direction}!`);
  }


  select(ret: {}): void {
    console.log('nzSelectChange', ret);
  }

  change(ret : TransferChange): void {
    
    const listKeys = ret.list.map(l => l.title);
  
    this.selectedCollection = listKeys
    
  }
  export(){
    console.log( this.selectedCollection);
    
    if (!this.selectedCollection) {
      this.createNotification("warning", "Vui lòng chọn  collection để export");
    } else {


      this.confirmation.info(`Are you sure to export collection ${this.selectedCollection}`   ,"are you sure").subscribe((status) => {
        if (status ===Confirmation.Status.confirm) {
          this.dataManagementService.exportToJsonByCollectionNames(this.selectedCollection).subscribe(() => {
            this.getData();
            this.createNotification("success", "Export thành công");
          });
        }
      })
    }
  }

  createNotification(type: string , content): void {
    this.notification.create(
      type,
      'Thông báo',
      content
    );
  }


}
