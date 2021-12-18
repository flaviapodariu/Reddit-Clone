import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor() { }

  email = new FormControl('', [Validators.required, Validators.email]);
  getErrorMessage() {
   if (this.email.hasError('required')) {
     return 'You must enter a valid email';
   }
   return this.email.hasError('email') ? 'Not a valid email' : '';
 }
  ngOnInit(): void {
  }

}
