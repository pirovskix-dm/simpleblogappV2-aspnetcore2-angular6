import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import '../../utils/extensions';
import {Query} from '../../models/query';
import {TableColumn} from '../../interfaces/table-column';
import {CategoryService} from '../../services/category.service';
import {CategoryModel} from '../../models/category-view-model';
import {DatePipe} from '@angular/common';
import {Router} from '@angular/router';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

	public totalItems = 0;
	public posts: PostModel[] = [];
	public categories: CategoryModel[] = [];
	public searchString = ``;
	public infoMessage = `Loading...`;
	pageSizeFilter: number[] = [ 5, 10, 15, 20 ];
	filters: any = {};
	query: Query = {
		search: ``,
		searchBy: [`Title`, `Content`, `ShortContent`],
		filters: [],
		sortBy: ``,
		isSortAscending: true,
		page: 1,
		pageSize: this.pageSizeFilter[0]
	};
	columns: TableColumn[] = [
		{ title: `#`, key: `Id`, isSortable: true },
		{ title: `Title`, key: `Title`, isSortable: true },
		{ title: `Created`, key: `DateCreated`, isSortable: true },
		{ title: `Updated`, key: `DateLastUpdated`, isSortable: true }
	];
	constructor(
		private postService: PostService,
		private categoryService: CategoryService,
		private router: Router
	) {
	}

	public ngOnInit(): void {
		this.populatePostsAsync();
		this.populateCategoriesAsync();
	}

	public dateFormat(date: string | null): string {
		if (!date) {
			return ``;
		}

		return new DatePipe(`en-US`).transform(date, `dd.MM.yyyy`) || ``;
	}

	public async deleteAsync(id: number): Promise<void> {
		await this.postService.deleteAsync(id);
		await this.populatePostsAsync();
	}

	public onSearch(): void {
		if (this.searchString.length > 3) {
			this.query.search = this.searchString;
			this.populatePostsAsync();
		} else if (this.query.search.length > 3) {
			this.query.search = ``;
			this.populatePostsAsync();
		}
	}

	public sortBy(column: string): void {
		if (this.query.sortBy !== column) {
			this.query.sortBy = column;
			this.query.isSortAscending = true;
		} else if (!this.query.isSortAscending) {
			this.query.sortBy = ``;
		} else {
			this.query.isSortAscending = !this.query.isSortAscending;
		}
		this.populatePostsAsync();
	}

	public onPagination(page: number): void {
		this.query.page = page;
		this.populatePostsAsync();
	}

	public onPageSizeChange(): void {
		this.query.page = 1;
		this.populatePostsAsync();
	}

	public onCategoriesFilterChange(): void {
		this.populatePostsAsync();
	}

	private createFilter(): string[] {
		const newFilters: string[] = [];
		for (let prop in this.filters) {
			const val = this.filters[prop];
			if (val)
				newFilters.push(encodeURIComponent(prop) + ':' + encodeURIComponent(val));
		}
		return newFilters;
	}

	private async populatePostsAsync(): Promise<void> {
		this.query.filters = this.createFilter();

		try {
			const result = await this.postService.getAdminPostsAsync(this.query);
			this.totalItems = result.totalItems;
			this.posts = result.items;
			this.infoMessage = `nothing found`;
		} catch (e) {
			console.error(e);
			this.router.navigate([`/login`]);
		}
	}

	private async populateCategoriesAsync(): Promise<void> {
		this.categories = await this.categoryService.getCategoriesAsync();
	}
}
