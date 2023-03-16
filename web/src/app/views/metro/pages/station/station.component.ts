import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Station } from '../../../../core/models/station/station.model';
import { HttpService } from '../../../../core/services/http.service';
import { RoutingService } from '../../../../core/services/routing.service';

@Component({
  selector: 'app-station',
  templateUrl: './station.component.html',
  styleUrls: ['./station.component.scss']
})
export class StationComponent implements OnInit {
  role: any = localStorage.getItem('role');
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
    private navigateService: RoutingService,
    private router: Router
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

  addOrEdit(id: string | undefined | null){
    debugger;
    this.navigateService.navigate(
      '/metro/station/' + (id != null ? 'edit/' + id : 'add'),
      id != null ? 'Edit Station' : 'Add Station'
    );
  }

  onDelete(id: string){
    this.httpService
      .delete(
        environment.Base_URL_Metro,
        `Station/${id}`, id
      ).subscribe({
        next: (res) => {
          console.log("success");
          this.router.navigate(['station']);
        }, error: (err) => {
          console.log(err);
        }
      })
  }

}
