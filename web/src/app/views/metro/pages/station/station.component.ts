import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Subject } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Station } from '../../../../core/models/station/station.model';
import { HttpService } from '../../../../core/services/http.service';

@Component({
  selector: 'app-station',
  templateUrl: './station.component.html',
  styleUrls: ['./station.component.scss']
})
export class StationComponent implements OnInit {
  
  dtTrigger: Subject<any> = new Subject<any>();
  metroHost: string = environment.Base_URL_Metro;
  stationList: Station[] = [
    {
      id: '',
      stationName: ''
    },
  ];

  constructor(
    private httpService : HttpService,
    private formBuilder: FormBuilder,
  ){}

  ngOnInit(): void {
    this.loadData(null);
  }

  loadData(id: any){
    this.httpService.get(this.metroHost, 'Station')
    .subscribe({
      next: (res) => {
        var result = res as any;
        this.stationList = result.items;
        console.log(this.stationList);
      }, error: (err) => {
        console.log(err);
      },
    });
  }

}
