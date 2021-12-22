import { Injectable } from '@angular/core';

export interface PostResponse {
  id: number;
  userId: number;
  upvotes: number;
  downvotes: number;
  title: string;
  text: string;
  user: {
    email: string;
    id: number;
    userName: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  private apiUrl = 'https://localhost:5001';
  constructor() {}

  getPosts() {
    return fetch(`${this.apiUrl}/post`, {
      method: 'GET',
    }).then((response) => response.json());
  }
  getPostById(id: number) {
    return fetch(`${this.apiUrl}/post/` + id, {
      method: 'GET',
    }).then((response) => response.json());
  }
}
