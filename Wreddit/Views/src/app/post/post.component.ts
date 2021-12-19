import { Component, OnInit, Input } from '@angular/core';
import { PostResponse } from '../posts.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  constructor() {}

  @Input() post: any;
  ngOnInit(): void {}
}
