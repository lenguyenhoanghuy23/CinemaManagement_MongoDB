import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DataManagementService } from '@proxy/data-management';
import { NzNotificationService } from 'ng-zorro-antd/notification';
declare var $: any;

@Component({
  selector: 'app-backup',
  templateUrl: './backup.component.html',
  styleUrls: ['./backup.component.scss']
})


export class BackupComponent implements OnInit {
  form:FormGroup;
  file:File
  constructor(
    private dataManagementService : DataManagementService,
    private confirmation: ConfirmationService,
    private notification: NzNotificationService,
    private fb:FormBuilder,

  ) { }

  ngOnInit(): void {
      this.buildFrom()
  }

  // buildForm
  buildFrom(){
    this.form = this.fb.group({
      backupDirectory :[ null ],
      dbName:[ null]
    })
  }

  backupSave(){
    this.confirmation.info(`Are you sure to backup ?`   ,"are you sure").subscribe((status) => {
      if (status ===Confirmation.Status.confirm) {
        this.dataManagementService.backup().subscribe(() => {
          this.createNotification("success", "backup thành công");
        });
      }
    })
  }
  createNotification(type: string , content): void {
    this.notification.create(
      type,
      'Thông báo',
      content
    );
  }


  restoreSave(){
    console.log(this.form.value);
    this.confirmation.info(`Are you sure Create reStore ?`   ,"are you sure").subscribe((status) => {
      if (status ===Confirmation.Status.confirm) {
        this.dataManagementService.reStoreByBackupDirectoryAndDbName(this.form.value.backupDirectory ,this.form.value.dbName).subscribe(() => {
          // this.createNotification("success", "restore thành công");
        });
      }
    })
  }


}
