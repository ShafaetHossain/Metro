import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { environment } from '../../../../../../environments/environment';
import { HttpService } from '../../../../../core/services/http.service';
import { RoutingService } from '../../../../../core/services/routing.service';

@Component({
  selector: 'app-station-add-edit',
  templateUrl: './station-add-edit.component.html',
  styleUrls: ['./station-add-edit.component.scss']
})
export class StationAddEditComponent implements OnInit{
  role: any = localStorage.getItem('role');
  stationId: string = '';
  isAddMode: boolean = true;

  stationForm! : FormGroup;

  constructor(
    private httpService: HttpService,
    private activateRouter: ActivatedRoute,
    private fb: FormBuilder,
    private navigateService: RoutingService
  ){
    this.stationForm = this.fb.group({
      id: [''],
      stationName: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    console.log(localStorage.getItem('userInfo'));
    this.activateRouter.paramMap.subscribe((params) => {
      debugger;
      this.stationId = params.get('id') || '';
      debugger;
      if(this.stationId == '') this.isAddMode = true;
      else this.isAddMode = false;
      this.initData();
    })
  }

  initData(){  
    if(!this.isAddMode){
      debugger;
      this.httpService
        .get(
          environment.Base_URL_Metro, 'Station/' + this.stationId
        ).subscribe({
          next: (res) => {
            const station = res as any;
            this.stationForm.patchValue(station);
          }, error: (err) => {
            console.log(err);
          }
        })
    }
  }

  onSubmit(): void{
    console.log("Form data: ", this.stationForm.value);
    if(this.stationForm.valid){
      if(!this.isAddMode){
        this.httpService
        .put(
          environment.Base_URL_Metro,
          `Station/${this.stationForm.value.id}`,
          this.stationForm.value,
          null
        )
        .subscribe({
          next:(res) => {
            Swal.fire('Station Update', 'Successfully!', 'success');
            this.navigateService.navigate(
              '/metro/station/',
              'Station'
            );
          },
          error:(err) => {
            Swal.fire('', 'Something went wrong!', 'error');
            // debugger;
            // console.log(err);
          }
        })
      } else {
        this.httpService.post(
          environment.Base_URL_Metro,
          'Station',
          this.stationForm.value
        )
        .subscribe({
          next:(res) => {
            Swal.fire('Station Added', 'Successfully!', 'success');
            this.navigateService.navigate(
              '/metro/station/',
              'Station'
            );
          },
          error:(err) => {
            Swal.fire('', 'Something went wrong!', 'error');
            // debugger;
            // console.log(err);
          }
        })
      }
    }
  }

}
