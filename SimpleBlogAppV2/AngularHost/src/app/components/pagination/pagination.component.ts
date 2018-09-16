import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';

@Component({
	selector: 'app-pagination',
	templateUrl: './pagination.component.html',
	styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnChanges {
	@Input(`total-items`) totalItems = 0;
	@Input(`page-size`) pageSize = 10;
	@Input(`current-page`) currentPage = 1;
	@Output(`page-changed`) pageChanged: EventEmitter<number> = new EventEmitter<number>();

	public pages: number[] = [];

	constructor() {
	}

	public ngOnChanges(): void {
		this.pages = [];
		const pagesCount = Math.ceil(this.totalItems /  this.pageSize);
		for (let i = 1; i <= pagesCount; i++) {
			this.pages.push(i);
		}
	}

	public changePage(page: number): void {
		this.currentPage = page;
		this.sendPageChangedNotification();
	}

	public previous(): void {
		if (this.currentPage < 2)
			return;

		this.currentPage--;
		this.sendPageChangedNotification();
	}

	public  next(): void {
		if (this.currentPage > this.pages.length - 1)
			return;

		this.currentPage++;
		this.sendPageChangedNotification();
	}

	private sendPageChangedNotification(): void {
		this.pageChanged.emit(this.currentPage);
	}
}
