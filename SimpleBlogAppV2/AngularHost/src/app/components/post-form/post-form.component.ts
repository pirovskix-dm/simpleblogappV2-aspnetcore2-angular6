import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
	selector: 'app-post-form',
	templateUrl: './post-form.component.html',
	styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {

	public id = 0;
	public pageTitle = '';
	public viewModel: PostViewModel;

	constructor(
		private postService: PostService,
		private route: ActivatedRoute,
		private router: Router
	) {
		this.viewModel = new PostViewModel();
	}

	public ngOnInit(): void {
		this.route.params.subscribe(p => {
			this.id = +p['id'] || 0;
			this.pageTitle = this.id ? 'Edit Post' : 'Create Post';

			if (this.id > 1)
				this.populatePost();
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

	private populatePost(): void {
		this.postService
			.getPost(this.id)
			.subscribe(post => {
					this.viewModel.Model = post;
				}, err => {
					console.error(`Fail populating posts`, err);
					this.router.navigate(['/admin']);
				});
	}
}


