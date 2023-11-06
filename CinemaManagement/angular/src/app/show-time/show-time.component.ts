import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CinemaZoomService, cinemaZoomDto } from '@proxy/cinema-zooms';
import { movieDto, movieService } from '@proxy/movies';
import { screenTypeDto, screenTypeService } from '@proxy/screen-types';
import { showTimeDto, showTimeService } from '@proxy/show-times';

import { NzI18nService, en_US, vi_VN } from 'ng-zorro-antd/i18n';


@Component({
  selector: 'app-show-time',
  templateUrl: './show-time.component.html',
  styleUrls: ['./show-time.component.scss'],
  providers:[
    ListService, 
  ]
})
export class ShowTimeComponent implements OnInit {

  showtime ={ items:[] , totalCount:0 } as PagedResultDto<showTimeDto>;
  movie = {items:[] , totalCount:0 } as PagedResultDto<movieDto>
  cinema = {items:[] , totalCount:0 } as PagedResultDto<cinemaZoomDto>
  screen = {items:[] , totalCount:0 } as PagedResultDto<screenTypeDto>

  isVisible =false;
  form:FormGroup;

  listOfOptionMovie: Array<{ value: string; label: string }> = [];
  listOfOptionCinema: Array<{ value: string; label: string }> = [];
  listOfOptionScreen: Array<{ value: string; label: string }> = [];

  public selectedShowTime ={} as showTimeDto
  
  

  constructor(
    public readonly list: ListService ,
    private showTimeService: showTimeService,
    private movieService: movieService,
    private cinemaService : CinemaZoomService,
    private ScreentypeService: screenTypeService,
    private fb:FormBuilder,
    private confirmation: ConfirmationService,
    private i18n: NzI18nService,
  
  ) {
    this.i18n.setLocale(vi_VN);
    this.i18n.setLocale( en_US );
  }


  ngOnInit(): void {
    
    const showtimeStreamCreator = (query) => this.showTimeService.getlist(query);
    this.list.hookToQuery(showtimeStreamCreator).subscribe((response) =>{
      this.showtime = response;
    })

    const movieStreamScreator =(query) => this.movieService.getlist(query);
    this.list.hookToQuery(movieStreamScreator).subscribe((response) => {
        this.movie = response
    })
    const cinemaStreamScreator =(query) => this.cinemaService.getlist(query);
    this.list.hookToQuery(cinemaStreamScreator).subscribe((response) => {
        this.cinema = response
    })
    const screenStreamScreator =(query) => this.ScreentypeService.getlist(query);
    this.list.hookToQuery(screenStreamScreator).subscribe((response) => {
        this.screen = response
    })

  
    
  }



  buildForm(){
    this.form = this.fb.group({
      showTimeCode:[this.selectedShowTime.showTimeCode ||null ],
      price:[this.selectedShowTime.price ||null ],
      movieSchedule:[this.selectedShowTime.movieSchedule ||null ],
      movie:[this.selectedShowTime.movie ||   null ],
      cinemaZoom:[this.selectedShowTime.cinemaZoom ||null ],
      screenType:[this.selectedShowTime.screenType ||null ],

    })
  }

  showModal(){
    this.isVisible =true;
    this.buildForm();
    this.selectedShowTime ={} as showTimeDto 

    this.listOfOptionMovie = this.movie.items.map(item => ({
      value: item.movieCode,
      label: item.movieName 
    }));

    this.listOfOptionCinema = this.cinema.items.filter(item  => item.status == 0).map(item => ({
      value: item.cinemaCode,
      label: item.cinemaName 
    }));

    this.listOfOptionScreen = this.screen.items.map(item => ({
      value: item.screenCode,
      label: item.screenName 
    }));

  }

  save(){
    console.log(this.form.value);

    const req =  this.selectedShowTime.showTimeCode
    ? this.showTimeService.update(this.selectedShowTime.showTimeCode , this.form.value)
    : this.showTimeService.create(this.form.value);

    req.subscribe(() =>{
      this.form.reset();
      this.isVisible=false;
      this.list.get();
    })
  }

  
  //close Modal create
  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }


  editShowtime(showTimeCode:string){
    console.log(showTimeCode);
    this.showTimeService.get(showTimeCode).subscribe((showtime) =>{
      this.selectedShowTime = showtime;
      this.isVisible = true;
      this.listOfOptionMovie = this.movie.items.map(item => ({
        value: item.movieCode,
        label: item.movieName 
      }));
      this.listOfOptionCinema = this.cinema.items.filter(item  => item.status == 0).map(item => ({
        value: item.cinemaCode,
        label: item.cinemaName 
      }));
      this.listOfOptionScreen = this.screen.items.map(item => ({
        value: item.screenCode,
        label: item.screenName 
      }));
      this.buildForm();
    })
  }

  deleteShowtime(showTimeCode:string){
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) =>{
      if (status === Confirmation.Status.confirm) {
        this.showTimeService.delete(showTimeCode).subscribe(() =>this.list.get());
      }
    })
  }

}
