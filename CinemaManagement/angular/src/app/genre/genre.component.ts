import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { genreDto, genreService } from '@proxy/genres';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.scss'],
  providers:[
    ListService, 
  ]
})
export class GenreComponent implements OnInit{
  genre ={ items:[] , totalCount:0 } as PagedResultDto<genreDto>;
  isModalOpen =false;
  form:FormGroup;
  selectedGenre ={} as genreDto 

  constructor(
    public readonly list: ListService ,
    private genreService: genreService,
    private fb:FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const genreStreamCreator = (query) => this.genreService.getlist(query);
    this.list.hookToQuery(genreStreamCreator).subscribe((genre) =>{
      this.genre = genre;
    })
  }


  // formBuilder
  buildForm(){
    this.form = this.fb.group({
      genreCode:[ this.selectedGenre.genreCode||null],
      genreName:[this.selectedGenre.genreName||null],
    })
  }

 // create 
  CreateGenre(){
    this.selectedGenre ={}as genreDto;
    this.buildForm();
    this.isModalOpen = true;
  }

   // edit 
  editGenre(genreCode:string){
    this.genreService.get(genreCode).subscribe((genre) =>{
      this.selectedGenre = genre;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

   // delete
  deleteGenre(genreCode:string){
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) =>{
      if (status === Confirmation.Status.confirm) {
        this.genreService.delete(genreCode).subscribe(() =>this.list.get());
        
      }
    })
  }

    //save
    save(){
      if (this.form.invalid) {
        return;
      }
      
      const repuest = this.selectedGenre.genreCode
        ? this.genreService.update(this.selectedGenre.genreCode , this.form.value)
        : this.genreService.create(this.form.value);
      
        repuest.subscribe(()=>{
          this.isModalOpen= false;
          this.form.reset();
          this.list.get();
      })
    }

}   
