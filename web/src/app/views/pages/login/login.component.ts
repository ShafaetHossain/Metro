import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { HttpService } from '../../../core/services/http.service';
import { RoutingService } from '../../../core/services/routing.service';
import { LoginResponse } from '../../../core/models/authenticate/loginResponse.model'
import { NgToastService } from 'ng-angular-popup';

import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginResponse: LoginResponse|any;

  loginForm!:FormGroup;
  constructor(
    private httpService: HttpService,
    private fb: FormBuilder,
    private router: Router,
    private navigateService: RoutingService,
    private toaster: NgToastService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onClickLogin(){
    if(this.loginForm.valid){
      this.httpService.post(
        environment.Base_URL_Metro,
        'user/authenticate',
        this.loginForm.value
      ).subscribe({
        next: (res) => {
          this.loginResponse = res as LoginResponse;
          // console.log(this.loginResponse.result.id)
          // console.log(this.loginResponse.result.role)
          localStorage.setItem('userinfoId', this.loginResponse.result.id);
          localStorage.setItem('role', this.loginResponse.result.role);
          //console.log("response", this.loginResponse);
          // console.log('localStorage',localStorage.getItem('userinfoId'));
          //console.log('localStorage',localStorage.getItem('role'));
          Swal.fire('User Login', 'Successful!', 'success');
          
          this.navigateService.navigate(
            '/dashboard/',
            'Dashboard'
          );
        }, error:(err) => {
          Swal.fire('', 'Something went wrong!', 'error');
          // Swal.fire({  
          //   position: 'top-end',  
          //   icon: 'error',  
          //   title: 'Something went wrong',  
          //   showConfirmButton: false,  
          //   timer: 1500  
          // })  
        }
      })
    }
    else{
      console.log("Failed");
      this.loginForm.markAllAsTouched();
    }
  }

}
