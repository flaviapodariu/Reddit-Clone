import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostResponse, PostsService } from '../posts.service';

@Component({
  selector: 'app-view-post',
  templateUrl: './view-post.component.html',
  styleUrls: ['./view-post.component.scss'],
})
export class ViewPostComponent implements OnInit {
  postId: number;

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
  constructor(
    private activateRoute: ActivatedRoute,
    private router: Router,
    private postsService: PostsService
  ) {
    this.postId = this.activateRoute.snapshot.params['id'];
    console.log(this.postId);
  }

  ngOnInit(): void {
    this.postsService.getPostById(this.postId).then((response) => {
      console.log(response);
      this.post = response;
    });
  }
}
