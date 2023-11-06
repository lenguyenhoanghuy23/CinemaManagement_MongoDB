import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { SellerService } from '@proxy/seller';
import { showTimeDto } from '@proxy/show-times';
import {NzI18nService, en_US, vi_VN } from 'ng-zorro-antd/i18n';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { tickerService, ticketDto } from '@proxy/tickets';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-seller',
  templateUrl: './seller.component.html',
  styleUrls: ['./seller.component.scss']
})
export class SellerComponent implements OnInit {
  seller = {items:[] , totalCount:0 } as PagedResultDto<showTimeDto>
  ticket ={ items:[] , totalCount:0 } as PagedResultDto<ticketDto>;

  public selectedShowTime ={} as showTimeDto

  selectedButtons: boolean[] = [];

  resetButtonState = false;
  resetButtons = false;
  // public rows = [{ type: '', items: [] } ];

  tempo= [];
  public rows: any[] = []; 

  date = new Date();
  movieAvate ="";
  totalAmount=0;

  constructor(
    private i18n: NzI18nService,
    private sellerService: SellerService,
    private modalService: NgbModal,
    private ticketService: tickerService,
    private notification: NzNotificationService
  ) {
    this.i18n.setLocale(vi_VN);
    // this.i18n.setLocale( en_US );
  }

  ngOnInit(): void {
    console.log(this.onChange(this.date));
    
  }

  onChange(result: Date): void {
    console.log('onChange: ', result.toLocaleDateString());

    if (result == null) {
      // Lấy thời gian hệ thống
      result = new Date();
    }

    
    this.sellerService.getlistScheduler(result.toDateString()).subscribe((seller ) =>{
      // this.seller = seller;
      seller.items = seller.items.filter((status) => status.status > 0);
      this.seller = seller;
    })
  }

  openModalBuyTIcket(showtimeCode:string , movieCode :string , avata :string, content){
      const regex = /^[A-Z]\d+$/;

      // console.log( this.rows.find((row) => row.showTimeCode ===showtimeCode))
      if (this.rows.length > 0) {
          this.rows =[],
          this.tempo =[],
          this.totalAmount= 0
      }
      this.movieAvate = avata;
      console.log(showtimeCode , movieCode, this.movieAvate);

      this.modalService.open(content, { fullscreen: true  ,modalDialogClass: 'dark-modal' } );
        this.ticketService.get(showtimeCode).subscribe((ticket) =>{
          this.ticket =  ticket;
          for (let i = 0; i < ticket.items.length; i++) {
            var info = ticket.items[i]

            if (regex.test(info.seatcode)) {
              const type = info.seatcode[0];
              this.addToRows(type, info.seatcode , info.showtime , info.price , info.status);
            }
            
          }
      })
      console.log(this.rows);
  }

  addToRows(type: string, item: any , showTimeCode:string , priceticker , status:number ) {
    // Tìm xem loại dòng đã tồn tại trong mảng rows chưa
    
    const existingRow = this.rows.find((row) => row.type === type);
    
    if (existingRow) {
      // Nếu loại dòng đã tồn tại, thêm item vào mảng items của loại dòng đó
      existingRow.items.push({ item, status });
    } else {
      // Nếu loại dòng chưa tồn tại, tạo một loại dòng mới và thêm item vào mảng items của loại dòng đó
      this.rows.push(
        { type: type, 
          items: [{ item, status }],
          showTimeCode:showTimeCode,
          priceticker:priceticker,
          status:status,
          disabled:status ===1
        });
    }
  }

  infoTicketSeate(showtime , seatsCode , priceticker ,event){

    const existingRow = this.tempo.find((tempo) => tempo.seatsCode == seatsCode );
  
    if (existingRow) {
      // Nếu ghế đã tồn tại, bạn xóa nó và thay đổi lớp CSS của nút
      this.tempo = this.tempo.filter((tempo) => tempo.seatsCode !== seatsCode);
      event.target.classList.remove('btn-danger');
      event.target.classList.add('btn-outline-secondary');
      this.totalAmount -= priceticker 
    } else {
      // Nếu ghế chưa tồn tại, bạn thêm nó và thay đổi lớp CSS của nút
      this.tempo.push({ showtime, seatsCode, priceticker });
      this.totalAmount += priceticker 
      event.target.classList.remove('btn-outline-secondary');
      event.target.classList.add('btn-danger');
    }
     // Kiểm tra xem có vé nào được chọn không
    
  }

  OrderTicket(){
    console.log(this.tempo);
    this.sellerService.update(this.tempo).subscribe(()=>{
      this.modalService.dismissAll(); // Đóng tất cả các modals
    })
    this. createNotification('success');
  }

  createNotification(type: string): void {
    this.notification.create(
      type,
      'Notification Title',
      'Đặt vé thành công'
    );
  }

}
