import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { screenTypeDto, screenTypeService } from '@proxy/screen-types';

@Component({
  selector: 'app-screen-type',
  templateUrl: './screen-type.component.html',
  styleUrls: ['./screen-type.component.scss'],
  providers:[
    ListService
  ]
})
export class ScreenTypeComponent implements OnInit{
  screentype = {items:[] , totalCount:0 } as PagedResultDto<screenTypeDto>
  isVisible = false;
  form:FormGroup;
  selectedScreenType ={} as screenTypeDto

  constructor(
    public readonly list: ListService, 
    private ScreentypeService: screenTypeService,
    private fb:FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const screentypeStreamScreator =(query) => this.ScreentypeService.getlist(query);
    this.list.hookToQuery(screentypeStreamScreator).subscribe((response) => {
      this.screentype = response
    })
  }

  // buildForm
  buildForm(){
    this.form = this.fb.group({
      screenCode:[ this.selectedScreenType.screenCode|| null],
      screenName:[this.selectedScreenType.screenName|| null],
    })
  }

  // -- edit
  editScreenType(screenCode:string){
    this.ScreentypeService.get(screenCode).subscribe((screenType)=>{
        this.selectedScreenType = screenType;
        this.buildForm();
        this.isVisible = true
        
    })
  }

  // --delete
  delete(screenCode:string){
    this.confirmation.warn("Are you sure to delete" ,"are you sure").subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.ScreentypeService.delete(screenCode).subscribe(() => this.list.get())
      }
    })
  }

  // show modal
  showModal(): void {
    this.isVisible = true;
    this.buildForm();
  }

  handleOk(): void {
    this.isVisible = false;
    console.log(this.form.value);
    
    const req= this.selectedScreenType.screenCode 
    ? this.ScreentypeService.update(this.selectedScreenType.screenCode,  this.form.value)
    : this.ScreentypeService.create(this.form.value)

    req.subscribe(()=>{
      this.isVisible= false;
      this.form.reset();
      this.list.get();
  })

  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }
  //end show modal
}
