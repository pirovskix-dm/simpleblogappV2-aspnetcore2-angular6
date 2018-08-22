import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
	selector: 'app-post-view',
	templateUrl: './post-view.component.html',
	styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {

	private id = 0;
	public post: PostViewModel;
	public pageNotFound = false;

	constructor(
		private postService: PostService,
		private router: Router,
		private route: ActivatedRoute
	) {
		this.post = new PostViewModel();
	}

	public ngOnInit() {
		this.route.params.subscribe(p => {
			this.id = +p['id'] || 0;
			this.populatePost();
		});
	}

	private populatePost(): void {
		this.postService.getPost(this.id)
			.subscribe(post => {
				this.post.Model = post;
			}, err => {
				this.pageNotFound = true;
			});
	}

}
