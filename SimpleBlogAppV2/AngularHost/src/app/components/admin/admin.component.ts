import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import '../../utils/extensions';
import {Query} from '../../models/query';
import {TableColumn} from '../../interfaces/table-column';
import {CategoryService} from '../../services/category.service';
import {CategoryModel} from '../../models/category-view-model';

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
		private categoryService: CategoryService
	) {
	}

	public ngOnInit(): void {
		this.populatePosts();
		this.populateCategories();
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

	public  sortBy(column: string): void {
		if (this.query.sortBy !== column) {
			this.query.sortBy = column;
			this.query.isSortAscending = true;
		} else if (!this.query.isSortAscending) {
			this.query.sortBy = ``;
		} else {
			this.query.isSortAscending = !this.query.isSortAscending;
		}
		this.populatePosts();
	}

	public onPagination(page: number): void {
		this.query.page = page;
		this.populatePosts();
	}

	public onPageSizeChange(): void {
		this.query.page = 1;
		this.populatePosts();
	}

	public onCategoriesFilterChange(): void {
		this.populatePosts();
	}

	private createFilter(): string[] {
		const newFilters: string[] = [];
		for(let prop in this.filters) {
			const val = this.filters[prop];
			if (val)
				newFilters.push(encodeURIComponent(prop) + ':' + encodeURIComponent(val));
		}
		return newFilters;
	}

	private populatePosts(): void {
		this.query.filters = this.createFilter();
		this.postService.getAdminPosts(this.query)
			.subscribe(qr => {
				this.totalItems = qr.totalItems;
				this.posts = qr.items;
				this.infoMessage = `nothing found`;
			}, err => console.error(`Fail populating posts: ${err.error.Message}`, err));
	}

	private populateCategories(): void {
		this.categoryService.getCategories()
			.subscribe(cs => {
				this.categories = cs;
			}, err => console.error(`Fail populating categories: ${err.error.Message}`, err));
	}
}
