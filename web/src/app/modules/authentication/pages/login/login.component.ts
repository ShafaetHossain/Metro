import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../../../../environments/environment';
import { HttpService } from '../../../../core/services/http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"

  loginForm!:FormGroup;
  constructor(
    private fb: FormBuilder,
    public httpService: HttpService,
    //  private userservice: UserService,
      // private router: Router,
      // private toast: NgToastService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onClickLogin(){
    console.log("herer");
    if(this.loginForm.valid){
      console.log("dasdasd", this.loginForm.value);
      this.httpService.login(
        environment.Base_URL_Metro,
        'user/authenticate',
        this.loginForm.value
      ).subscribe({
        next:(res) => {
          console.log("success");
        },
        error:(err) => {
          console.log("failed");
        }
      })
    }
    else{
      console.log("Failed");
      this.loginForm.markAllAsTouched();
    }
  }


  public testing: boolean = true;

}
