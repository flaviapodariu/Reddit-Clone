import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  constructor() {
   }

   email = new FormControl('', [Validators.required, Validators.email]);
   getEmailError() {
    if (this.email.hasError('required'))
        return 'You must enter a valid email';

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }
  
  // password = new FormControl('', [Validators.required]);
  // getPasswordError(){
  //   if (this.password.hasError('required'))
  //       return 'You must enter a password';
  //   //call from api (check pwd against DB)
  //   // if password not correct display message  
  // 
  // }
  

  ngOnInit(): void {
  }

}
