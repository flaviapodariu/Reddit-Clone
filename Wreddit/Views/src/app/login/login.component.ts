import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChildActivationStart, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  constructor(private authService: AuthenticationService, private router: Router) {}

  private route = '';

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)])                 
  });

  onSubmit(){
    let credentials = this.loginForm.value;
    this.authService.login(credentials)
        .subscribe((res: any) => { 
           if(res && res.token){
              this.storeToken(res.token);
              this.router.navigate(['/'])  //change to user_role-dashboard
           }
           });
        
  }

  private storeToken(token: string){
    localStorage.setItem("token", token);
  }

  ngOnInit(): void {
  }

}
