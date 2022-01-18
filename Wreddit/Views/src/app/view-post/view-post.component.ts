import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  PostResponse,
  PostsService,
  PostVote,
} from '../services/posts.service';
import {
  CommentsService,
  CommentToCreate,
  CommentResponse,
  CommentVote,
} from '../services/comments.service';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-view-post',
  templateUrl: './view-post.component.html',
  styleUrls: ['./view-post.component.scss'],
})
export class ViewPostComponent implements OnInit {
  postId: number;
  content = new FormControl('', [Validators.required]); // the comment textarea value
  isVoted: number = 0;
  post!: PostResponse;
  comments: CommentResponse[] = [];
  commentVotes: CommentVote[] = [];
  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private postService: PostsService,
    public authService: AuthenticationService,
    private commentService: CommentsService
  ) {
    this.postId = this.activateRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.postService.getPostById(this.postId).then((response) => {
      // the the post the user clicked on
      this.post = response;
    });
    this.postService.getPostVotesFromUser().subscribe((res: PostVote[]) => {
      let postVote = res.find((vote) => vote.postId === this.post.id); // checks if the post is voted in postVotes
      if (postVote) {
        this.isVoted = postVote.voteType;
      }
    });
    this.commentService
      .getCommentVotesFromUser(this.postId)
      .subscribe((res: CommentVote[]) => {
        this.commentVotes = res;
      });
    this.commentService.getCommentsFromPost(this.postId).then((response) => {
      // all the comments for the post
      this.comments = response;
    });
  }

  handleCreateCommentClick(): void {
    // when the user clicks on the add comment button

    if (!this.authService.authStatus()) {
      this.router.navigate(['/login']);
      alert('You have to be logged in to comment');
    }

    if (this.content.value) {
      // if the comment is not empty
      const comment: CommentToCreate = {
        parentId: 0,
        userId: this.authService.getUserId(),
        content: this.content.value,
        postId: this.postId,
      };

      this.commentService.createComment(comment).subscribe((res: any) => {
        const newComment: CommentResponse = {
          // comment is of type commentToCreate so I need to change the type in commentResponse in order to add it to comments and update the UI
          ...comment,
          id: res.id,
          upvotes: 0,
          downvotes: 0,
          user: {
            id: this.authService.getUserId(),
            userName: this.authService.getUsername(),
            email: '',
          },
        };
        this.comments.unshift(newComment);
        this.content.reset();
      });
    }
  }

  votePost(postId: number, voteType: number) {
    if (this.authService.authStatus()) {
      this.postService.votePost(postId, voteType).subscribe((res: PostVote) => {
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

  deletePost(postId: number){
    this.postService.deletePost(postId).subscribe((res:any) => console.log(res));
    this.router.navigate(['/']);
  }
}
