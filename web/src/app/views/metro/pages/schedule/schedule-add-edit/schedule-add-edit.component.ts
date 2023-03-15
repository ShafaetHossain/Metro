import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../../../environments/environment';
import { Station } from '../../../../../core/models/station/station.model';
import { HttpService } from '../../../../../core/services/http.service';
import { RoutingService } from '../../../../../core/services/routing.service';

@Component({
  selector: 'app-schedule-add-edit',
  templateUrl: './schedule-add-edit.component.html',
  styleUrls: ['./schedule-add-edit.component.scss']
})
export class ScheduleAddEditComponent implements OnInit {
  scheduleId: string = '';
  stationList: Station[] = [];
  isAddMode: boolean = true;

  scheduleForm!: FormGroup;

  constructor(
    private httpService: HttpService,
    private activateRouter: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private navigateService: RoutingService
  ) {
    this.scheduleForm = this.fb.group({
      id: [''],
      stationFromId: ["", Validators.required],
      stationToId: ["", Validators.required],
      departureTime: ["", Validators.required],
      totalSeat: [0, Validators.required],
      seatBooked: [0, Validators.required],
      price: [0, Validators.required]
    })
  }

  ngOnInit(): void {
    this.activateRouter.paramMap.subscribe((params) => {
      this.scheduleId = params.get('id') || '';
      if(this.scheduleId == '') this.isAddMode = true;
      else this.isAddMode = false;
      this.initData();
    })
  }

  loadSchedule(){
    this.httpService
      .get(environment.Base_URL_Metro, 'Station')
      .subscribe({
        next: (res) => {
          var result = res as any;
          this.stationList = result.items;
          console.log(this.stationList);
        }, error(err){
          console.log(err);
        }
      })
  }

  initData(){  
    this.loadSchedule();
    if(!this.isAddMode){
      debugger;
      this.httpService
        .get(
          environment.Base_URL_Metro, 'Schedule/' + this.scheduleId
        ).subscribe({
          next: (res) => {
            const schedule = res as any;
            this.scheduleForm.patchValue(schedule);
          }, error: (err) => {
            console.log(err);
          }
        })
    }
  }

  onSubmit(): void{
    console.log("Form data: ", this.scheduleForm.value);
    if(this.scheduleForm.valid){
      if(!this.isAddMode){
        this.httpService
        .put(
          environment.Base_URL_Metro,
          `Schedule/${this.scheduleForm.value.id}`,
          this.scheduleForm.value,
          null
        )
        .subscribe({
          next:(res) => {
            this.navigateService.navigate(
              '/metro/schedule/',
              'Schedule'
            );
          },
          error:(err) => {
            debugger;
            console.log(err);
          }
        })
      } else {
        this.httpService.post(
          environment.Base_URL_Metro,
          'Schedule',
          this.scheduleForm.value
        )
        .subscribe({
          next:(res) => {
            this.navigateService.navigate(
              '/metro/schedule/',
              'Schedule'
            );
          },
          error:(err) => {
            debugger;
            console.log(err);
          }
        })
      }
    }
  }
}
