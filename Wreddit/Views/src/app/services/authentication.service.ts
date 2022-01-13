import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ThrowStmt } from '@angular/compiler';

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

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }  

  public visibility = false;
  status(){
    if(this.isLoggedIn() && this.hasRole('User'))
       this.visibility = true;
    this.visibility = false;   

  }
  register(newUser: SignupData){
    return this.http.post(`${this.apiUrl}/signup`, JSON.stringify(newUser),
                           this.httpOptions);  
  }

  login(user: LoginData){
   return this.http.post(`${this.apiUrl}/login`, JSON.stringify(user),
                          this.httpOptions);
  }
  
  logout(){
    if(localStorage.getItem("token")){
       localStorage.removeItem("token");
       this.visibility = true;
    }
  }

  isLoggedIn(){
    if(!localStorage.getItem("token"))
       return false;

    let jwtHelper: JwtHelperService = new JwtHelperService()
    let token = localStorage.getItem("token");
    return !jwtHelper.isTokenExpired(token!)   // token! = tells ts token can't be null
  } 
  
  hasRole(wantedRole: string){
    if(!localStorage.getItem("token"))
       return false;
    
    let jwtHelper: JwtHelperService = new JwtHelperService()   
    let token = localStorage.getItem("token");
    return(jwtHelper.decodeToken(token!).role.includes(wantedRole));
  }
  
  getToken(){
    return localStorage.getItem("token");
  }
  
  getUserId(){
    let token = localStorage.getItem("token");
    let jwtHelper: JwtHelperService = new JwtHelperService()  
    return jwtHelper.decodeToken(token!).nameid;
  }
  getUsername(){
    let token = localStorage.getItem("token");
    let jwtHelper: JwtHelperService = new JwtHelperService();
    return jwtHelper.decodeToken(token!).email;
  }
}
