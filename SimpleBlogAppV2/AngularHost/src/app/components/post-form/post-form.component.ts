import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CategoryModel} from '../../models/category-view-model';
import {CategoryService} from '../../services/category.service';

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

	public ngOnInit(): void {
		this.route.params.subscribe(p => {
			this.id = +p['id'] || 0;
			this.pageTitle = this.id ? 'Edit Post' : 'Create Post';

			this.populateForm().then().catch(err => {
				console.error(`Fail populating form: `, err.message ? err.message : err);
				this.router.navigate(['/admin']).then();
			});

		});
	}

	public onSubmit(): void {
		if (this.viewModel.Form.invalid)
			return;

		if (this.id < 1)
			this.createPost();
		else
			this.updatePost();
	}

	public delete(): void {
		if (this.id < 1)
			return;

		this.deletePost();
	}

	public dateFormat(date: string): string {
		return date.toDDMMYYYY(`.`);
	}

	public isCategorySelected(category: CategoryModel | null): boolean {
		if (!category || !this.viewModel.get(`category`))
			return false;

		return category.id === this.viewModel.get(`category`).id;
	}

	private updatePost(): void {
		this.postService
			.update(this.id, this.viewModel.Model)
			.subscribe(id => console.log(`save success`),
				err => console.error(`Fail updating posts`, err));
	}

	private createPost(): void {
		this.postService
			.create(this.viewModel.Model)
			.subscribe(id => this.router.navigate(['/admin']),
				err => console.error(`Fail creating posts`, err));
	}

	private  deletePost(): void {
		this.postService
			.delete(this.id)
			.subscribe(id => this.router.navigate(['/admin']),
				err => console.error(`Fail updating posts`, err));
	}

	private async populateForm(): Promise<void> {
		this.categories = await this.categoryService.getCategoriesAsync();
		if (this.id > 1)
			this.viewModel.Model = await this.postService.getPostAsync(this.id);
	}
}


