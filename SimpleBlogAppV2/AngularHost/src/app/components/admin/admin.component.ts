import {Component, OnInit} from '@angular/core';
import {PostModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';
import '../../utils/extensions';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

	public totalPosts = 0;
	public posts: PostModel[] = [];
	public infoMessage = `Loading...`;

	constructor(
		private postService: PostService
	) {
	}

	public ngOnInit(): void {
		this.populatePosts();
	}

	public dateFormat(date: string | null): string {
		if (date === null) {
			return ``;
		}
		return date.toDDMMYYYY(`.`);
	}

	public delete(id: number): void {
		this.postService.delete(id).subscribe(i => {
			this.populatePosts();
		}, err => console.error(`Fail deleting posts`, err));
	}

	private populatePosts(): void {
		this.postService.getAdminPosts()
			.subscribe(ps => {
				this.totalPosts = ps.length;
				this.posts = ps;
				this.infoMessage = `nothing found`;
			}, err => console.error(`Fail populating posts`, err));
	}
}
