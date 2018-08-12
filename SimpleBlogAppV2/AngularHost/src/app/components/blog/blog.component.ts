import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';

@Component({
	selector: 'app-blog',
	templateUrl: './blog.component.html',
	styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
	posts: PostViewModel[] = [];

	constructor(
		private  postService: PostService
	) {}

	ngOnInit() {
		this.populatePosts();
	}

	private populatePosts(): void {
		this.postService
			.getPosts()
			.subscribe(p => {
				this.posts = p;
			}, err => console.error(`Fail populating posts`, err));
	}
}
