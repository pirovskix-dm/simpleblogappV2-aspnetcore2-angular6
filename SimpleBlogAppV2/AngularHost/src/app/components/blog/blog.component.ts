import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';

@Component({
	selector: 'app-blog',
	templateUrl: './blog.component.html',
	styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

	public posts: PostModel[] = [];

	constructor(
		private  postService: PostService
	) {}

	public ngOnInit() {
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
