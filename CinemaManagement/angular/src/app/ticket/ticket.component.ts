import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { showTimeDto,showTimeService } from '@proxy/show-times';
import { tickerService, ticketDto } from '@proxy/tickets';



@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
  providers:[
    ListService, 
  ]
})
export class TicketComponent  implements OnInit{

  showtime ={ items:[] , totalCount:0 } as PagedResultDto<showTimeDto>;
  ticket ={ items:[] , totalCount:0 } as PagedResultDto<ticketDto>;


  public selectedShowTime ={} as showTimeDto


  constructor(
      public readonly list: ListService ,
      private showTimeService: showTimeService,
      private ticketService: tickerService,
      private confirmation: ConfirmationService,
  ){}

  ngOnInit(): void {
    const showtimeStreamCreator = (query) => this.showTimeService.getlist(query);
    this.list.hookToQuery(showtimeStreamCreator).subscribe((response) =>{
      this.showtime = response;
    })
    

  }


  createTicket(showtimeCode:string){
  
      console.log(showtimeCode);
      this.ticketService.create(showtimeCode).subscribe(() =>{})
    
  }

  ShowDetailTickWithShowitme(showTimeCode:string){
    console.log(showTimeCode);
    this.ticketService.get(showTimeCode).subscribe((ticket) =>{
      console.log(ticket);
      this.ticket =  ticket;
    })
    
  }


  deleteTicket(showTimeCode:string){
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) =>{
      if (status === Confirmation.Status.confirm) {
        this.ticketService.delete(showTimeCode).subscribe(() => this.list.get());
      }
    })
    console.log(this.list.get());
  }


  

}
