import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CommentsService } from 'src/app/services/comments.service';
import { PostResponse, PostsService } from 'src/app/services/posts.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.scss']
})
export class UserDashboardComponent implements OnInit {

  constructor(private postService: PostsService,
              private commService: CommentsService,
              private userService: UserService,
              public authService: AuthenticationService,
              private router: Router) { }

  posts: PostResponse[] = [];
  comms: number = 0;
  ngOnInit(): void {
     this.postService.getPostsByUser(this.authService.getUserId())
         .subscribe((res:any) => this.posts = res);
  }
  deleteAccount(){
    this.userService.deleteUser(this.authService.getUserId())
        .subscribe((res:any) => {
          console.log(res)
          this.router.navigate(['/']);
        }
    );
  }
}
