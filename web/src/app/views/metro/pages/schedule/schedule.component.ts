import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Schedule } from '../../../../core/models/schedule/schedule.model';
import { Station } from '../../../../core/models/station/station.model';
import { HttpService } from '../../../../core/services/http.service';
import { RoutingService } from '../../../../core/services/routing.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  role: any = localStorage.getItem('role');
  dtTrigger: Subject<any> = new Subject<any>();
  metroHost: string = environment.Base_URL_Metro;
  scheduleList: Schedule[] = [];
  stationList: Station[] = []

  map = new Map();

  constructor(
    private httpService : HttpService,
    private formBuilder: FormBuilder,
    private navigateService: RoutingService,
    private router: Router
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

    this.httpService.get(this.metroHost, 'Station')
    .subscribe({
      next: (res) => {
        var result = res as any;
        this.stationList = result.items;
        for (var station of this.stationList) {
          this.map.set(station.id, station.stationName);
        }
        console.log(this.stationList);
      }, error: (err) => {
        console.log(err);
      },
    });
  }

  addOrEdit(id: string | undefined | null){
    this.navigateService.navigate(
      '/metro/schedule/' + (id != null ? 'edit/' + id : 'add'),
      id != null ? 'Edit Schedule' : 'Add Schedule'
    );
  }

  onDelete(id: string){
    this.httpService
      .delete(
        environment.Base_URL_Metro,
        `Schedule/${id}`, id
      ).subscribe({
        next: (res) => {
          console.log("success");
          this.router.navigate(['schedule']);
        }, error: (err) => {
          console.log(err);
        }
      })
  }

}
