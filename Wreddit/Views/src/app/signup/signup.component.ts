import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor(private service: AuthenticationService) {
   }
  
  emailCheck = new FormControl('', [Validators.required, Validators.email]);

  getErrorMessage() {
   if (this.emailCheck.hasError('required')) {
     return 'You must enter a valid email';
   }
   return this.emailCheck.hasError('email') ? 'Not a valid email' : '';
 }


  signupForm = new FormGroup({
    email: new FormControl(''),
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])                 
  });
  onSubmit(){
    let newUser = this.signupForm.value;
    this.service.register(newUser)
        .subscribe(u => {alert('success'); });
  }
  ngOnInit(): void {
  }

}
