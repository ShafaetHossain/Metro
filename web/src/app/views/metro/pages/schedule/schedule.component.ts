import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Subject } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Schedule } from '../../../../core/models/schedule/schedule.model';
import { HttpService } from '../../../../core/services/http.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  dtTrigger: Subject<any> = new Subject<any>();
  metroHost: string = environment.Base_URL_Metro;
  scheduleList: Schedule[] = [];

  constructor(
    private httpService : HttpService,
    private formBuilder: FormBuilder,
  ){}

  ngOnInit(): void {
    this.loadData(null);
  }
  
  loadData(id: any){
    this.httpService.get(this.metroHost, 'Schedule')
    .subscribe({
      next: (res) => {
        var result = res as any;
        this.scheduleList = result.items;
        console.log(this.scheduleList);
      }, error: (err) => {
        console.log(err);
      },
    });
  }

}