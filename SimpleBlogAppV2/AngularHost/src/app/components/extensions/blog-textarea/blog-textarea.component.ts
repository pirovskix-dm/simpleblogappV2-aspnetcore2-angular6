import {Component, Input, OnChanges} from '@angular/core';
import {PostViewModel} from '../../../models/post-view-model';

@Component({
	selector: 'app-blog-textarea',
	templateUrl: './blog-textarea.component.html',
	styleUrls: ['./blog-textarea.component.css']
})
export class BlogTextareaComponent implements OnChanges {
	@Input(`bName`) bName = ``;
	@Input(`bLabel`) bLabel = ``;
	@Input(`bRows`) bRows = `3`;
	@Input(`bViewModel`) bViewModel: PostViewModel | null = null;

	constructor() {
	}

	public ngOnChanges(): void {

	}

	public validateControl(): boolean {
		if (this.bViewModel === null)
			return false;

		return this.bViewModel.validate(this.bName);
	}
}
