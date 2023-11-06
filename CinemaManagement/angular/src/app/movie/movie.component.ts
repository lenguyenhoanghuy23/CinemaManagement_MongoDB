import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { movieDto, movieService } from '@proxy/movies';
import { NzI18nService, en_US, vi_VN } from 'ng-zorro-antd/i18n';
import {  NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { genreDto, genreService } from '@proxy/genres';
import { CountrieService, countrieDto } from '@proxy/countries';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss'],
  providers:[
    ListService
  ]
})
export class MovieComponent implements OnInit {
  movie = {items:[] , totalCount:0 } as PagedResultDto<movieDto>
  genre ={ items:[] , totalCount:0 } as PagedResultDto<genreDto>;
  countrie ={ items:[] , totalCount:0 } as PagedResultDto<countrieDto>;

  form:FormGroup;
  public selectedMovie ={} as movieDto
  isVisible = false;
  

  // detail modal movie 
  openModelDetail =false;

  listOfOptionGenre: Array<{ value: string; label: string }> = [];
  listOfOptionCountrie: Array<{ value: string; label: string }> = [];


  imageUrl: string = "";
  file: File;

  constructor(
    public readonly list: ListService, 
    private movieService: movieService,
    private genreService: genreService,
    private countrieService: CountrieService,
    private fb:FormBuilder,
    private confirmation: ConfirmationService,
    private i18n: NzI18nService,
    private modalService: NgbModal,

  ) {
    this.i18n.setLocale(vi_VN);
    this.i18n.setLocale( en_US );
  }

  ngOnInit(): void {
    const movieStreamScreator =(query) => this.movieService.getlist(query);
    this.list.hookToQuery(movieStreamScreator).subscribe((response) => {
        this.movie = response
    })

    const genreStreamCreator = (query) => this.genreService.getlist(query);
    this.list.hookToQuery(genreStreamCreator).subscribe((response) =>{
      this.genre = response;
      
    })

    const countrieStreamCreator = (query) => this.countrieService.getlist(query);
    this.list.hookToQuery(countrieStreamCreator).subscribe((response) =>{
      this.countrie = response;
      
    })
  }

  buildForm(){
    this.form = this.fb.group({
      movieCode:[this.selectedMovie.movieCode ||null ],
      movieName:[this.selectedMovie.movieName ||null ],
      description:[this.selectedMovie.description ||null ],
      genres:[this.selectedMovie.genres ||null ],
      countries:[this.selectedMovie.countries ||null ],
      releaseDate:[this.selectedMovie.releaseDate ||null ],
      endDate:[this.selectedMovie.endDate ||null ],
      producer:[this.selectedMovie.producer ||null ],
      actor:[this.selectedMovie.actor ||null ],
      director:[this.selectedMovie.director ||null ],
      runningTime:[this.selectedMovie.runningTime ||null ],
      year:[2023 ],
      TemporaryDate: [ ],
    })

    this.form.get('TemporaryDate').valueChanges.subscribe((dates: Date[]) => {
      // Lấy giá trị TemporaryDate từ mảng dates
      const [startDate, endDate] = dates;
      this.form.patchValue({ releaseDate: startDate }); // Gán giá trị releaseDate
      this.form.patchValue({ endDate: endDate }); // Gán giá trị endDate
    });
  }

  // func show modal
  showModal(): void {
    this.isVisible = true;
    this.selectedMovie ={} as movieDto 

    this.listOfOptionGenre = this.genre.items.map(item => ({
      value: item.genreCode,
      label: item.genreName
    }));

    this.listOfOptionCountrie = this.countrie.items.map(item => ({
      value: item.countrieCode,
      label: item.countrieName
    }));

    this.buildForm()
  }

  loadPage(pageCurrent: number) {
    this.movieService.getlist({
      sorting: '',
      skipCount: (pageCurrent - 1) * 10,
      maxResultCount: 10,
    }).subscribe((response) => {
      this.movie = response;
    });
  }

    // detail movie
    detail(id:string , content){
      this.modalService.open(content, { size: 'lg' });
  
      this.movieService.get(id).subscribe((movie) =>{
        this.openModelDetail =true,
        this.selectedMovie = movie
        console.log(this.selectedMovie);
      })
    }

    save() {
      console.log(this.form.value);
      console.log(this.file);
      
      this.movieService.create( this.file ,this.form.value ).subscribe(() =>{
        this.form.reset();
        this.isVisible=false;
        this.list.get();
      })
    }

    DeleteMovie(movieCode:string){
        console.log(movieCode);
        this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) =>{
          if (status === Confirmation.Status.confirm) {
            this.movieService.delete(movieCode).subscribe(() =>this.list.get());
          }
        })
        
    }

    // -- edit
    editMovie(screenCode: string) {
      this.movieService.get(screenCode).subscribe(movie => {
        this.selectedMovie = movie;
        this.imageUrl = "https://localhost:44333/"+ movie.avata
        this.buildForm();
        this.isVisible = true;
      });
    }
     //close Modal create
    handleCancel(): void {
      console.log('Button cancel clicked!');
      this.isVisible = false;
    }

      // upload Img
    onFileSelected(event){
      const file:File = event.target.files[0];
      if (file == undefined) {  
      }
      if (file) {
        this.file = file;
      }
    }

    onFileSelectedImg(event: any) {
      const file = event.target.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.imageUrl = e.target.result;
          this.file = file
      };
        reader.readAsDataURL(file);
      } else {
        this.imageUrl = "";
      }
    }

    clearImg(){
      this.imageUrl = "";
      this.file = null; // Xóa lựa chọn hình ảnh
      const fileInput = document.querySelector(".file-input") as HTMLInputElement;
      if (fileInput) {
        fileInput.value = ""; 
      }
    }
   
}
