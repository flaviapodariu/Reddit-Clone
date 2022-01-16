import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

export interface CommentResponse {
  id: number;
  parentId: number;
  userId: number;
  content: string;
  postId: number;
  upvotes: number;
  downvotes: number;
  user: {
    id: number;
    userName: string;
    email: string;
  };
}

export interface CommentToCreate {
  parentId: number;
  userId: number;
  content: string;
  postId: number;
}

export interface CommentVote {
  commentId: number;
  userId: number;
  voteType: number;
}

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  private apiUrl = 'https://localhost:5001';

  constructor(
    private http: HttpClient,
    private authService: AuthenticationService
  ) {}

  getCommentsFromPost(postId: number) {
    return fetch(`${this.apiUrl}/comment/` + postId, {
      method: 'GET',
    }).then((response) => response.json());
  }

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `${this.authService.getToken()}`,
    }),
  };

  createComment(comment: CommentToCreate) {
    return this.http.post(
      `${this.apiUrl}/comment`,
      JSON.stringify(comment),
      this.httpOptions
    );
  }

  voteComment(commentId: number, vote: number) {
    let user = this.authService.getUserId();
    let data = { commentId: `${commentId}`, userId: `${user}`, voteType: vote };
    console.log(JSON.stringify(data));
    return this.http.put<CommentVote>(
      `${this.apiUrl}/comment-votes`,
      JSON.stringify(data),
      this.httpOptions
    );
  }
  getCommentVotesFromUser(postId: number) {
    let userId = this.authService.getUserId();
    return this.http.get<CommentVote[]>(
      `${this.apiUrl}/comment-votes/${postId}/${userId}`,
      this.httpOptions
    );
  }
}
