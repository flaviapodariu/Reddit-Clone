import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

export interface PostResponse {
  id: number;
  userId: number;
  upvotes: number;
  downvotes: number;
  title: string;
  text: string;
  user: {
    id: number;
    userName: string;
    email: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  private apiUrl = 'https://localhost:5001';
  constructor(private http: HttpClient, private authService: AuthenticationService) {}

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json-patch+json',
      'Authorization': `${this.authService.getToken()}`
    })
  }  

  // getPosts() {
  //   return fetch(`${this.apiUrl}/post`, {
  //     method: 'GET',
  //   }).then((response) => response.json()).catch((error) => console.log(error) );
  // }
  getPosts(){
    return this.http.get(`${this.apiUrl}/post`, this.httpOptions)
  }

  getPostById(id: number){
    return fetch(`${this.apiUrl}/post/` + id, {
      method: 'GET',
    }).then((response) => response.json());
  }

  votePost(id: number, vote: string){
   let data = {"postId": `${id}`, "voteType": vote};
   console.log(JSON.stringify(data))
   return this.http.put(`${this.apiUrl}/post`, JSON.stringify(data), this.httpOptions)
  }
}
