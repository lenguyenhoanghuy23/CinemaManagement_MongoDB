import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CinemaZoomService, cinemaZoomDto } from '@proxy/cinema-zooms';
import { statusEmuOptions } from '@proxy/status';

@Component({
  selector: 'app-cinema-zoom',
  templateUrl: './cinema-zoom.component.html',
  styleUrls: ['./cinema-zoom.component.scss'],
  providers:[
    ListService
  ]
})
export class CinemaZoomComponent {
  cinemaZoom ={items:[] , totalCount:0 } as PagedResultDto<cinemaZoomDto>
  
  isModalOpen =false;
  form:FormGroup;
  selectedCiname ={} as cinemaZoomDto
  statusOption = statusEmuOptions
  constructor (
    public readonly list: ListService, 
    private cinemaService : CinemaZoomService,
    private confirmation: ConfirmationService,
    private fb:FormBuilder,
    
  ){}

  ngOnInit(): void {
    const cinemaZoomStreamScreator =(query) => this.cinemaService.getlist(query);
    this.list.hookToQuery(cinemaZoomStreamScreator).subscribe((response) => {
      this.cinemaZoom = response
    }) 

  }


     // --buildForm
    buildForm(){
    this.form = this.fb.group({
      cinemaCode:[ this.selectedCiname.cinemaCode||null],
      cinemaName:[this.selectedCiname.cinemaName||null],
      numberSeats:[this.selectedCiname.numberSeats||null ],
      rowSeats:[this.selectedCiname.rowSeats||null],
      columnSeats:[this.selectedCiname.columnSeats||null],
      status:[this.selectedCiname.status||null],
    })
  }
  
    createCinme(){
      this.selectedCiname = {} as cinemaZoomDto
      this.isModalOpen = true;
      this.buildForm();
    }


    save() {
      // if (this.form.invalid) {
      //   return;
      // }
  
      console.log(this.form.value);
      const repuest = this.selectedCiname.cinemaCode
        ? this.cinemaService.update(this.selectedCiname.cinemaCode , this.form.value)
        : this.cinemaService.create(this.form.value)
        repuest.subscribe(()=>{
          this.isModalOpen= false;
          this.form.reset();
          this.list.get();
      })
    }
  


    // -- edit
  editScreenType(cinemaCode: string) {
    this.cinemaService.get(cinemaCode).subscribe(cinemaZoom => {
      this.selectedCiname = cinemaZoom;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // --delete
  delete(screenCode: string) {
    this.confirmation.warn('Are you sure to delete', 'are you sure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.cinemaService.delete(screenCode).subscribe(() => this.list.get());
      }
    });
  }


    calculateTotal() {
      const rowSeatsValue = this.form.get('rowSeats').value;
      const columnSeatsValue = this.form.get('columnSeats').value;
    
      if (rowSeatsValue !== null && columnSeatsValue !== null) {
        const total = rowSeatsValue * columnSeatsValue;
        this.form.get('numberSeats').setValue(total);
        console.log(total);
        
      }
    }
  
}
