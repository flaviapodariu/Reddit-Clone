import { Component, OnInit } from '@angular/core';
import { PostsService, PostResponse } from '../posts.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  posts: PostResponse[] = [];
  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
    this.postsService.getPosts().then((response: PostResponse[]) => {
      this.posts = response;
      console.log(this.posts);
    });
  }
}
