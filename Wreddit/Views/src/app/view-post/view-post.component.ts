import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostResponse, PostsService } from '../services/posts.service';
import {
  CommentsService,
  CommentToCreate,
  CommentResponse,
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
  post: PostResponse = {
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
  comments: CommentResponse[] = [];
  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private postService: PostsService,
    private authService: AuthenticationService,
    private commentService: CommentsService
  ) {
    this.postId = this.activateRoute.snapshot.params['id'];
    console.log(this.postId);
  }

  ngOnInit(): void {
    this.postService.getPostById(this.postId).then((response) => {
      // the the post the user clicked on
      this.post = response;
    });
    this.commentService.getCommentsFromPost(this.postId).then((response) => {
      // all the comments for the post
      console.log(response);
      this.comments = response;
    });
  }

  handleCreateCommentClick(): void {
     // when the user clicks on the add comment button

    if(!this.authService.authStatus())
       {
         this.router.navigate(['/login']);
         alert("You have to be logged in to comment");
       }

       if (this.content.value) {
         // if the comment is not empty
         const comment: CommentToCreate = {
           parentId: 0,
           userId: this.authService.getUserId(),
           content: this.content.value,
           postId: this.postId,       
         };

         this.commentService.createComment(comment).subscribe((res: any)=> {
           const newComment: CommentResponse = {
                  // comment is of type commentToCreate so I need to change the type in commentResponse in order to add it to comments and update the UI
                  ...comment,
                     upvotes: 0,
                     downvotes: 0,
                     user: {
                      id: this.authService.getUserId(),
                      userName: this.authService.getUsername(),
                      email: '',
                     },
                    };
                    console.log(newComment);
                    this.comments.unshift(newComment);
                    this.content.reset();
                  })
        }
      }
      
  votePost(postId: number, voteType: string){
    if(this.authService.authStatus()){
      this.postService.votePost(postId, voteType).subscribe((res: any)=> console.log(res));
    }
    else this.router.navigate(['/login']);
  }

  voteComment(commentId: number, voteType: string){
    if(this.authService.authStatus()){
      this.commentService.voteComment(commentId, voteType).subscribe((res: any)=> console.log(res));
    }
    else this.router.navigate(['/login']);
  }
}