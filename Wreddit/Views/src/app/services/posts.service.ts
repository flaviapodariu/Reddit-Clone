import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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

export interface PostToCreate {
  userId: number;
  title: string;
  text: string;
}

export interface PostVote {
  postId: number;
  userId: number;
  voteType: number;
}

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  private apiUrl = 'https://localhost:5001';
  constructor(
    private http: HttpClient,
    private authService: AuthenticationService
  ) {}

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `${this.authService.getToken()}`,
    }),
  };

  getPosts() {
    return this.http.get(`${this.apiUrl}/post`, this.httpOptions);
  }

  getPostById(id: number) {
    return fetch(`${this.apiUrl}/post/` + id, {
      method: 'GET',
    }).then((response) => response.json());
  }

  createPost(post: PostToCreate) {
    return this.http.post(
      `${this.apiUrl}/post`,
      JSON.stringify(post),
      this.httpOptions
    );
  }

  votePost(postId: number, vote: number) {
    let user = this.authService.getUserId();
    let data = { postId: `${postId}`, userId: `${user}`, voteType: vote };
    return this.http.put<PostVote>(
      `${this.apiUrl}/post`,
      JSON.stringify(data),
      this.httpOptions
    );
  }
  getPostVotesFromUser() {
    let userId = this.authService.getUserId();
    return this.http.get<PostVote[]>(
      `${this.apiUrl}/post/votes/${userId}`,
      this.httpOptions
    );
  }
}
