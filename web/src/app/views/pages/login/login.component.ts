import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { HttpService } from '../../../core/services/http.service';
import { RoutingService } from '../../../core/services/routing.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm!:FormGroup;
  constructor(
    private httpService: HttpService,
    private fb: FormBuilder,
    private router: Router,
    private navigateService: RoutingService
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
          console.log("success");
          this.navigateService.navigate(
            '/dashboard/',
            'Dashboard'
          );
        }, error:(err) => {
          console.log(err);
        }
      })
    }
    else{
      console.log("Failed");
      this.loginForm.markAllAsTouched();
    }
  }

}
