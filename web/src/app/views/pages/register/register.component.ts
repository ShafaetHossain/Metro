import { Component } from '@angular/core';
import { HttpService } from '../../../core/services/http.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm!:FormGroup
  constructor(
    private fb: FormBuilder,
    public httpService: HttpService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      role:['']
    })
  }

  onClickRegister(){
    console.log("herer", this.registerForm.value);
    if(this.registerForm.valid){
      this.httpService.post(
        environment.Base_URL_Metro,
        'user/register',
        this.registerForm.value
      )
      .subscribe({
        next:(res) => {
          Swal.fire('User Added', 'Successful!', 'success');
          this.router.navigate(['login']);
        },
        error:(err) => {
          Swal.fire('', 'Something went wrong!', 'error');
        }
      })
    }
    else{
      Swal.fire('', 'Something went wrong!', 'error');
      // console.log("Failed");
      // this.registerForm.markAllAsTouched();
    }
  }

}
