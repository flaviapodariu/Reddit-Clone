import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthenticationService) {}

  logout(){ 
    this.authService.logout();
  }
  clicked:boolean = false;

  clickStatus(){
    if(!this.clicked)
     this.clicked = true;
    else
      this.clicked = false; 
  }
  title = 'Wreddit';
}

