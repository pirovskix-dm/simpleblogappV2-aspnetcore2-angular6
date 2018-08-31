import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import '../../utils/extensions';
import {Query} from '../../models/query';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

	public totalItems = 0;
	public posts: PostModel[] = [];
	public searchString = ``;

	query: Query = {
		search: ``,
		searchBy: [`Title`, `Content`, `ShortContent`],
		sortBy: null,
		isSortAscending: true,
		page: 1,
		pageSize: 100
	};

	public infoMessage = `Loading...`;

	constructor(
		private postService: PostService
	) {
	}

	public ngOnInit(): void {
		this.populatePosts();
	}

	public dateFormat(date: string | null): string {
		if (date === null) {
			return ``;
		}
		return date.toDDMMYYYY(`.`);
	}

	public delete(id: number): void {
		this.postService.delete(id).subscribe(i => {
			this.populatePosts();
		}, err => console.error(`Fail deleting posts: ${err.error.Message}`, err));
	}

	public onSearch(): void {
		if (this.searchString.length > 3) {
			this.query.search = this.searchString;
			this.populatePosts();
		} else if (this.query.search.length > 3) {
			this.query.search = ``;
			this.populatePosts();
		}
	}

	private populatePosts(): void {
		this.postService.getAdminPosts(this.query)
			.subscribe(qr => {
				this.totalItems = qr.totalItems;
				this.posts = qr.items;
				this.infoMessage = `nothing found`;
			}, err => console.error(`Fail populating posts: ${err.error.Message}`, err));
	}
}
