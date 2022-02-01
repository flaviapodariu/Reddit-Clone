import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private authService: AuthenticationService,
              private http: HttpClient) { }       

  private apiUrl = 'https://localhost:5001';

  deleteUser(userToDelete: number)
  {
    let userId = this.authService.getUserId();
    let httpOptionsWithBody ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `${this.authService.getToken()}`,
      }),
      body:{"id": userToDelete, "userId": userId, "token": this.authService.getToken()}
    }
    

    return this.http.delete(`${this.apiUrl}/user`, httpOptionsWithBody);
  }
}
