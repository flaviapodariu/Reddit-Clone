import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { LoginComponent } from '../login/login.component';
export interface SignupData{
  email: string;
  userName: string;
  password: string;
}
export interface LoginData{
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiUrl = 'https://localhost:5001';
  constructor(private http: HttpClient) { }

  register(newUser: SignupData): Observable<SignupData>{
    return this.http.post<SignupData>(`${this.apiUrl}/signup`, JSON.stringify(newUser),
     {
         headers: new HttpHeaders({
        'Content-Type': 'application/json-patch+json'
      })
    });  
  }

  login(user: LoginData): Observable<LoginData>{
    return this.http.post<LoginData>(`${this.apiUrl}/login`, JSON.stringify(user),
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json-patch+json'
    })
  });
  }
  
}
