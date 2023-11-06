import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CountrieService, countrieDto } from '@proxy/countries';

@Component({
  selector: 'app-countrie',
  templateUrl: './countrie.component.html',
  styleUrls: ['./countrie.component.scss'],
  providers:[
    ListService
  ]
})
export class CountrieComponent {
  country = {
    items:[],
    totalCount:0
  }  as PagedResultDto<countrieDto>

  isModalOpen = false;
  form:FormGroup;
  selectedCountrie ={} as countrieDto


  constructor (
    public readonly list: ListService, 
    private countrieService: CountrieService,
    private fb:FormBuilder,
    private confirmation: ConfirmationService
  ){}

  ngOnInit(): void {
    const screentypeStreamScreator =(query) => this.countrieService.getlist(query);
    this.list.hookToQuery(screentypeStreamScreator).subscribe((response) => {
      this.country = response
    })
  }

    // create 
    createCountry (){
      this.selectedCountrie ={ } as countrieDto
      this.buildFrom();
      this.isModalOpen = true;
    }
  // edit
    editCountry(id:string){
      this.countrieService.get(id).subscribe((countrie) =>{
        this.selectedCountrie = countrie;
        this.buildFrom();
        this.isModalOpen = true;
      })
    }


  // delete
    deleteCountry(id:string){
      this.confirmation.warn("Are you sure to delete" ,"are you sure").subscribe((status) => {
        if (status ===Confirmation.Status.confirm) {
            this.countrieService.delete(id).subscribe(() => this.list.get());
        }
      })
    }
  // buildForm
    buildFrom(){
      this.form = this.fb.group({
        countrieCode :[
            this.selectedCountrie.countrieCode||null ],
        countrieName:[
            this.selectedCountrie.countrieName || null]
      })
    }
  // save
    save(){
      if (this.form.invalid) {
        return;
      }

      const repuest = this.selectedCountrie.countrieCode
      ? this.countrieService.update(this.selectedCountrie.countrieCode , this.form.value)
      : this.countrieService.create(this.form.value)

      repuest.subscribe(()=>{
        this.isModalOpen= false;
        this.form.reset();
        this.list.get();
    })

    }

}
