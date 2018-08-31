import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import {Query} from '../../models/query';

@Component({
	selector: 'app-blog',
	templateUrl: './blog.component.html',
	styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

	public totalItems = 0;
	public posts: PostModel[] = [];
	query: Query = {
		search: ``,
		searchBy: [],
		sortBy: null,
		isSortAscending: true,
		page: 1,
		pageSize: 2
	};

	constructor(
		private  postService: PostService
	) {}

	public ngOnInit() {
		this.populatePosts();
	}

	private populatePosts(): void {
		this.postService
			.getBlogPosts(this.query)
			.subscribe(qr => {
				console.log(`Populationg from service`);
				this.totalItems = qr.totalItems;
				this.posts = qr.items;
			}, err => console.error(`Fail populating posts`, err));
	}
}
