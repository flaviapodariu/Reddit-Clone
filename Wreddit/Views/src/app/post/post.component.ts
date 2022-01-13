import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { PostResponse, PostsService } from '../services/posts.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  constructor(private postService: PostsService, private router: Router, private authService: AuthenticationService) {}

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

  vote(postId: number, voteType: string){
    if(this.authService.isLoggedIn() && this.authService.hasRole("User")){
      this.postService.votePost(postId, voteType).subscribe((res: any)=> console.log(res));
    }
  }

  public goToPostPage(id: number): void {
    
    this.router.navigateByUrl('/view-post/' + id);
  }
  ngOnInit(): void {}
}
