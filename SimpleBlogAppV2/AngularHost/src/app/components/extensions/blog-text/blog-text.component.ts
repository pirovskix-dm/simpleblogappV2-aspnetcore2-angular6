import {Component, Input, OnChanges} from '@angular/core';

@Component({
	selector: 'app-blog-text',
	templateUrl: './blog-text.component.html',
	styleUrls: ['./blog-text.component.css']
})
export class BlogTextComponent implements OnChanges {
	@Input(`bName`) bName = ``;
	@Input(`bLabel`) bLabel = ``;
	@Input(`bValue`) bValue = ``;

	constructor() {
	}
	public ngOnChanges(): void {

	}
}