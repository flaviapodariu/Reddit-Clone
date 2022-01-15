import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';

export interface allUsers {
  userData:[{
     item1: string,
     item2: string
     }
  ],
  role: number,
  count: number
}

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = 'https://localhost:5001';
  constructor(private http: HttpClient, private authService: AuthenticationService) { }

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `${this.authService.getToken()}`
    })
  }
  getAllUsers(){
    return this.http.get(`${this.apiUrl}/admin-dashboard`,
    )
  }
}
