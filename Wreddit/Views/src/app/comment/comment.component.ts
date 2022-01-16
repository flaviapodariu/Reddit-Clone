import { Component, Input, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import {
  CommentResponse,
  CommentsService,
  CommentVote,
} from '../services/comments.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {
  constructor(
    private commentService: CommentsService,
    private authService: AuthenticationService
  ) {}
  isVoted: number = 0;
  @Input() comment!: CommentResponse;
  @Input() commentVotes!: CommentVote[];
  ngOnInit(): void {
    let commentVote = this.commentVotes.find(
      (vote) => vote.commentId === this.comment.id
    ); // checks if the comment is voted in postVotes
    if (commentVote) {
      this.isVoted = commentVote.voteType;
    }
  }

  voteComment(commentId: number, voteType: number) {
    if (this.authService.authStatus()) {
      this.commentService
        .voteComment(commentId, voteType)
        .subscribe((res: CommentVote) => {
          if (this.isVoted === 0) {
            // if the user didn't voted the post
            this.isVoted = res.voteType;
            if (res.voteType === 1) {
              this.comment.upvotes++;
            } else this.comment.downvotes++;
          } else if (this.isVoted === 1) {
            // if the user already upvoted the comment
            if (res.voteType === 1) {
              this.isVoted = 0;
              this.comment.upvotes--;
            } else if (res.voteType === -1) {
              this.isVoted = -1;
              this.comment.upvotes--;
              this.comment.downvotes++;
            }
          } else if (this.isVoted === -1) {
            // if the user already downvoted the comment
            if (res.voteType === -1) {
              this.isVoted = 0;
              this.comment.downvotes--;
            } else if (res.voteType === 1) {
              this.isVoted = 1;
              this.comment.upvotes++;
              this.comment.downvotes--;
            }
          }
        });
    }
  }
}
