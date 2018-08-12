import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {FormGroup, Validators, FormControl} from '@angular/forms';
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
	public postForm: FormGroup;
	public pvm: PostViewModel = {
		id: 0,
		title: ``,
		shortContent: ``,
		content: ``,
		dateCreated: ``,
		dateLastUpdated: ``
	};
	private controls: any = {
		title: new FormControl(``, [Validators.required, Validators.maxLength(100)]),
		shortContent: new FormControl(``, Validators.maxLength(500)),
		content: new FormControl(``, Validators.required)
	};

	constructor(
		private postService: PostService,
		private route: ActivatedRoute,
		private router: Router
	) {
		this.postForm = new FormGroup(this.controls);
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
		if (this.id > 0)
			this.updatePost();
		else
			this.createPost();
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
		this.readControls();
		this.postService
			.update(this.id, this.pvm)
			.subscribe(id => console.log(`save success`),
				err => console.error(`Fail updating posts`, err));
	}

	private createPost(): void {
		this.readControls();
		this.postService
			.create(this.pvm)
			.subscribe(id => this.router.navigate(['/admin']),
				err => console.error(`Fail updating posts`, err));
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
			.subscribe(post => this.initForm(post),
				err => {
					console.error(`Fail populating posts`, err);
					this.router.navigate(['/admin']);
				});
	}

	private initForm(post: PostViewModel): void {
		this.pvm = post;
		this.postForm.patchValue(post);
	}

	private readControls(): void {
		this.pvm.title = this.controls.title.value || ``;
		this.pvm.shortContent = this.controls.shortContent.value || ``;
		this.pvm.content = this.controls.content.value || ``;
	}
}


