import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './post/post.component';
import { ViewPostComponent } from './view-post/view-post.component';
import { AuthenticationService } from './services/authentication.service';
import { PostsService } from './services/posts.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { CreatePostComponent } from './create-post/create-post.component';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatIconModule} from '@angular/material/icon';
import { CommentComponent } from './comment/comment.component';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    HomeComponent,
    PostComponent,
    ViewPostComponent,
    CreatePostComponent,
    CommentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatExpansionModule,
    MatIconModule
  ],
  
  providers: 
    [AuthenticationService,
     PostsService,
     {
      provide : HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi   : true,
    }
  ],

  bootstrap: [AppComponent],
})
export class AppModule {}
