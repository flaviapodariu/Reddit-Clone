import { Component, OnInit } from '@angular/core';
import { PostsService, PostResponse } from '../posts.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  posts: PostResponse[] = [];
  constructor(private postsService: PostsService, private router: Router) {}

  ngOnInit(): void {
    this.postsService.getPosts().then((response: PostResponse[]) => {
      this.posts = response;
    });
  }
  public goToPostPage(id: number): void {
    this.router.navigateByUrl('/view-post/' + id);
  }
}
