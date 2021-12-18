import { Component, OnInit } from '@angular/core';
import { PostsService, PostResponse } from '../posts.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  posts: PostResponse[] = [];
  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
    this.postsService.getPosts().then((response: PostResponse[]) => {
      this.posts = response;
      console.log(this.posts);
    });
  }
}
