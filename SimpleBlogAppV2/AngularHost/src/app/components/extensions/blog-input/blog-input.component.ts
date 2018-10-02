import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';
import {PostViewModel} from '../../../models/post-view-model';

@Component({
	selector: 'app-blog-input',
	templateUrl: './blog-input.component.html',
	styleUrls: ['./blog-input.component.css']
})
export class BlogInputComponent implements OnChanges {
	@Input(`bName`) bName = ``;
	@Input(`bLabel`) bLabel = ``;
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
