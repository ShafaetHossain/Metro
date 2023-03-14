import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/authentication/pages/login/login.component';
import { RegisterComponent } from './modules/authentication/pages/register/register.component';

const routes: Routes = [
  // {
  //   path: 'dashboard',
  //   component: Dashboard,
  //   data: {
  //     title: 'Dashboard'
  //   }
  // }
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
