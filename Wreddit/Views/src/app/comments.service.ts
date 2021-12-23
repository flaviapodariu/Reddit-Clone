import { Injectable } from '@angular/core';

export interface CommentResponse {
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

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  private apiUrl = 'https://localhost:5001';

  constructor() {}

  getCommentsFromPost(postId: number) {
    return fetch(`${this.apiUrl}/comment/` + postId, {
      method: 'GET',
    }).then((response) => response.json());
  }

  createComment(comment: CommentToCreate) {
    return fetch(`${this.apiUrl}/comment`, {
      method: 'POST',
      headers: {
        // Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(comment),
    });
  }
}
