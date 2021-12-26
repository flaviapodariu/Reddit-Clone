import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  constructor(private authService: AuthenticationService, private router: Router) {}

  signupForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)])                 
  });
  
  onSubmit(){
    let newUser = this.signupForm.value;
    console.log(this.signupForm)
    if(this.signupForm.valid)
    this.authService.register(newUser)
        .subscribe((res: any) => {alert(res);
                    this.router.navigate(['/login']); });
  }
  
  ngOnInit(): void {
  }

}
