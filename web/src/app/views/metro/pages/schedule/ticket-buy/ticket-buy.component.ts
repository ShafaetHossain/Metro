import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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

  scheduleId: string = '';
  scheduleForm! : FormGroup;
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
      seatAvailable: ["", Validators.required],
      seatBook: [0, Validators.required],
    })
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
          console.log('id', this.scheduleId);
          console.log('res', schedule);
          this.scheduleForm.patchValue(schedule);
        }, error: (err) => {
          console.log(err);
        }
      })
  }

}
