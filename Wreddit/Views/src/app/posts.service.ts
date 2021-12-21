import { Injectable } from '@angular/core';

export interface PostResponse {
  id: number;
  username: string;
  upvotes: number;
  downvotes: number;
  title: string;
  text: string;
}

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  private apiUrl = 'https://localhost:5001';
  constructor() {}

  getPosts() {
    return fetch(`${this.apiUrl}/post/getPosts`, {
      method: 'GET',
    }).then((response) => response.json());
  }
}
