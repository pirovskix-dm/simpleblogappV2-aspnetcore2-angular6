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

	public async ngOnInit() {
		const params = await this.route.snapshot.params;
		this.id = +params['id'] || 0;

		try {
			this.post.Model = await this.postService.getPostAsync(this.id);
		} catch (err) {
			console.error(err.message);
			this.pageNotFound = true;
		}
	}

}
