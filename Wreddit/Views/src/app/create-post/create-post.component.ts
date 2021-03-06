import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { PostToCreate, PostsService } from '../services/posts.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.scss'],
})
export class CreatePostComponent implements OnInit {
  constructor(private postService: PostsService, 
              private router: Router,
              private authService: AuthenticationService) {}
  title = new FormControl('', [Validators.required]);
  text = new FormControl('', [Validators.required]);

  post: PostToCreate = {
    userId: 1,
    title: '',
    text: '',
  };
  ngOnInit(): void {}

  handleCreatePostClick(): void {
    this.post.userId = this.authService.getUserId();
    this.post.title = this.title.value;
    this.post.text = this.text.value;
    // this.postService.createPost(this.post).then((response) => {
    //   this.router.navigateByUrl('');
    // });
    this.postService.createPost(this.post).subscribe((res:any) => this.router.navigate(['/']));
  }
}
