import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  constructor(private service: AuthenticationService) {
   }

   email = new FormControl('', [Validators.required, Validators.email]);
   getEmailError() {
    if (this.email.hasError('required'))
        return 'You must enter a valid email';

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])                 
  });
  
  // password = new FormControl('', [Validators.required]);
  // getPasswordError(){
  //   if (this.password.hasError('required'))
  //       return 'You must enter a password';
  //   //call from api (check pwd against DB)
  //   // if password not correct display message  
  // 
  // }
  
  onSubmit(){
    let credentials = this.loginForm.value;
    this.service.login(credentials)
        .subscribe(u=> alert('logged in'));
  }

  ngOnInit(): void {
  }

}
