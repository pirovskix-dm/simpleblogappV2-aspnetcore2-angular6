import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CategoryModel} from '../../models/category-view-model';
import {CategoryService} from '../../services/category.service';
import {DatePipe} from '@angular/common';

@Component({
	selector: 'app-post-form',
	templateUrl: './post-form.component.html',
	styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {

	public id = 0;
	public pageTitle = '';
	public viewModel: PostViewModel;
	public categories: CategoryModel[] = [];

	constructor(
		private postService: PostService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private router: Router
	) {
		this.viewModel = new PostViewModel();
	}

	public async ngOnInit() {
		const params = this.route.snapshot.params;
		this.id = +params['id'] || 0;
		this.pageTitle = this.id ? 'Edit Post' : 'Create Post';

		try {
			await this.populateFormAsync();
		} catch (err) {
			console.error(`Fail populating form: `, err.message ? err.message : err);
			await this.router.navigate(['/admin']);
		}
	}

	public onSubmit(): void {
		if (this.viewModel.Form.invalid)
			return;

		if (this.id < 1)
			this.postService.createAsync(this.viewModel.Model);
		else
			this.postService.updateAsync(this.id, this.viewModel.Model);
	}

	public delete(): void {
		if (this.id < 1)
			return;

		this.postService.deleteAsync(this.id);
	}

	public dateFormat(date: string): string {
		if (!date) {
			return ``;
		}

		return new DatePipe(`en-US`).transform(date, `dd.MM.yyyy`) || ``;
	}

	public isCategorySelected(category: CategoryModel | null): boolean {
		if (!category || !this.viewModel.get(`category`))
			return false;

		return category.id === this.viewModel.get(`category`).id;
	}

	private async populateFormAsync(): Promise<void> {
		this.categories = await this.categoryService.getCategoriesAsync();
		if (this.id > 1)
			this.viewModel.Model = await this.postService.getPostAsync(this.id);
	}
}


