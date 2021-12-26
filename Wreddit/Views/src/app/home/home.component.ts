import { Component, OnInit } from '@angular/core';
import { PostsService, PostResponse } from '../services/posts.service';
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
    // this.postsService.getPosts().then((response: PostResponse[]) => {
    //   this.posts = response;
    // });

    this.postsService.getPosts().subscribe((res: any) =>{
      console.log(res);
      if(res)
        this.posts = res;
      else alert("You are not authorized!");  
    });
  }

  public goToPostPage(id: number): void {
    this.router.navigateByUrl('/view-post/' + id);
  }
}
