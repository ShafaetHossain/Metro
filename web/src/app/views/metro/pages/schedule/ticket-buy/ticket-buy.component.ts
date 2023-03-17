import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { environment } from '../../../../../../environments/environment';
import { Schedule } from '../../../../../core/models/schedule/schedule.model';
import { Station } from '../../../../../core/models/station/station.model';
import { HttpService } from '../../../../../core/services/http.service';
import { RoutingService } from '../../../../../core/services/routing.service';

@Component({
  selector: 'app-ticket-buy',
  templateUrl: './ticket-buy.component.html',
  styleUrls: ['./ticket-buy.component.scss']
})
export class TicketBuyComponent implements OnInit{

  userId: any = localStorage.getItem('userinfoId');
  scheduleId: string = '';
  scheduleForm! : FormGroup;
  ticketForm! : FormGroup;
  stationList: Station[] = [];
  map = new Map();
  from: string = '';
  to: string = '';
  seatAvailable: number = 0;

  constructor(
    private httpService: HttpService,
    private activateRouter: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private navigateService: RoutingService
  ){
    this.scheduleForm = this.fb.group({
      id: [''],
      stationFromId: ["", Validators.required],
      stationToId: ["", Validators.required],
      seatAvailable: [0, Validators.required],
      seatBook: [0, Validators.required],
    });
    this.ticketForm = this.fb.group({
      scheduleId: [''],
      userId: [''],
      buySeat: [0, Validators.required],
    });
  }

  ngOnInit(): void {
    this.activateRouter.paramMap.subscribe((params) => {
      this.scheduleId = params.get('id') || '';
      this.initData();
    })
  }

  loadStation(){
    this.httpService
      .get(environment.Base_URL_Metro, 'Station')
      .subscribe({
        next: (res) => {
          var result = res as any;
          this.stationList = result.items;
          for (var station of this.stationList) {
            this.map.set(station.id, station.stationName);
          }
        }, error(err){
          console.log(err);
        }
      })
  }

  initData(){  
    this.loadStation();
    this.httpService
      .get(
        environment.Base_URL_Metro, 'Schedule/' + this.scheduleId
      ).subscribe({
        next: (res) => {
          const schedule = res as Schedule;
          this.from = this.map.get(schedule.stationFromId);
          this.to = this.map.get(schedule.stationToId);
          this.seatAvailable = schedule.totalSeat - schedule.seatBooked;
          this.scheduleForm.patchValue(schedule);
        }, error: (err) => {
          console.log(err);
        }
      })
  }

  buy(){
    debugger;
    this.scheduleForm.value.seatAvailable = this.seatAvailable;
    if(this.scheduleForm.valid){
      if(this.scheduleForm.value.seatAvailable < this.scheduleForm.value.seatBook){
        Swal.fire('', 'Please buy valid amount of seat ticket', 'error');
      }
      else{
        this.ticketForm.value.scheduleId = this.scheduleId;
        this.ticketForm.value.userId = this.userId;
        this.ticketForm.value.buySeat = this.scheduleForm.value.seatBook;
        this.httpService.post(
          environment.Base_URL_Metro,
          'TicketInfo/buyTicket',
          this.ticketForm.value
        )
        .subscribe({
          next: (res) => {
            Swal.fire('Ticket Buy', 'Successfully!', 'success');
            this.navigateService.navigate(
              '/metro/schedule/',
              'Schedule'
            );
          },
          error:(err) => {
            Swal.fire('', 'Something went wrong!', 'error');
            // debugger;
            // console.log(err);
          }
        })
      }
    } else{
      console.log(this.scheduleForm.value);
      Swal.fire('', 'Something went wrong!', 'error');
    }
  }

}
