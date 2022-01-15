import { ConnectionPositionPair } from '@angular/cdk/overlay';
import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import {
  PostResponse,
  PostsService,
  PostVote,
} from '../services/posts.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  constructor(
    private postsService: PostsService,
    private router: Router,
    private authService: AuthenticationService
  ) {}

  isVoted: number = 0;
  @Input() post: PostResponse = {
    id: 0,
    userId: 0,
    upvotes: 0,
    downvotes: 0,
    title: '',
    text: '',
    user: {
      email: '',
      id: 0,
      userName: '',
    },
  };
  @Input()
  public postVotes!: PostVote[];

  votePost(postId: number, voteType: number) {
    if (this.authService.authStatus()) {
      this.postsService
        .votePost(postId, voteType)
        .subscribe((res: PostVote) => {
          if (this.isVoted === 0) {
            // if the user didn't voted the post
            this.isVoted = res.voteType;
            if (res.voteType === 1) {
              this.post.upvotes++;
            } else this.post.downvotes++;
          } else if (this.isVoted === 1) {
            // if the user already upvoted the post
            if (res.voteType === 1) {
              this.isVoted = 0;
              this.post.upvotes--;
            } else if (res.voteType === -1) {
              this.isVoted = -1;
              this.post.upvotes--;
              this.post.downvotes++;
            }
          } else if (this.isVoted === -1) {
            // if the user already downvoted the post
            if (res.voteType === -1) {
              this.isVoted = 0;
              this.post.downvotes--;
            } else if (res.voteType === 1) {
              this.isVoted = 1;
              this.post.upvotes++;
              this.post.downvotes--;
            }
          }
        });
    }
  }

  public goToPostPage(id: number): void {
    this.router.navigateByUrl('/view-post/' + id);
  }
  ngOnInit(): void {
    let postVote = this.postVotes.find((vote) => vote.postId === this.post.id); // checks if the post is voted in postVotes
    if (postVote) {
      this.isVoted = postVote.voteType;
    }
  }
}
