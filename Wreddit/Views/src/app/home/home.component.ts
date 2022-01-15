import { Component, OnInit } from '@angular/core';
import {
  PostsService,
  PostResponse,
  PostVote,
} from '../services/posts.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  posts: PostResponse[] = [];
  postVotes!: PostVote[];
  constructor(
    private postsService: PostsService,
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // this.postsService.getPosts().then((response: PostResponse[]) => {
    //   this.posts = response;
    // });

    this.postsService.getPosts().subscribe((res: any) => {
      if (res) this.posts = res;
      else alert('You are not authorized!');
    });
    this.postsService.getPostVotesFromUser().subscribe((res: PostVote[]) => {
      this.postVotes = res;
    });
  }

  public goToCreatePost(): void {
    this.router.navigateByUrl('/create-post');
  }
}
