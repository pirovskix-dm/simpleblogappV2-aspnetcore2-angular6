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
		filters: [],
		sortBy: ``,
		isSortAscending: true,
		page: 1,
		pageSize: 2
	};

	constructor(
		private  postService: PostService
	) {}

	public ngOnInit() {
		this.populatePostsAsync();
	}

	private async populatePostsAsync(): Promise<void> {
		const result = await this.postService.getBlogPostsAsync(this.query);
		this.totalItems = result.totalItems;
		this.posts = result.items;
	}
}
