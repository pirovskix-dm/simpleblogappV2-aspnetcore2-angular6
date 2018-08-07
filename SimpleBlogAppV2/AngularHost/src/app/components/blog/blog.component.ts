import {Component, OnInit} from '@angular/core';
import {PostViewModel, SavePostViewModel} from '../../models/post-view-model';
import {PostService} from '../../services/post.service';

@Component({
	selector: 'app-blog',
	templateUrl: './blog.component.html',
	styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
	posts: PostViewModel[] = [];
	post: PostViewModel = {
		id: 0,
		title: ``,
		content: ``,
		shortContent: ``,
		dateCreated: null,
		dateLastUpdated: null
	};
	savePost: SavePostViewModel = {
		title: `Hello title`,
		content: `Hello content`,
		shortContent: `Hello short conten`
	};

	constructor(
		private  postService: PostService
	) {}

	ngOnInit() {
		this.createPost();
		this.populatePost();
	}

	private populatePosts(): void {
		this.postService
			.getPosts()
			.subscribe(p => {
				this.posts = p;
			}, err => console.error(`Fail populating posts`, err));
	}

	private populatePost(): void {
		this.postService
			.getPost(2)
			.subscribe(p => {
				this.post = p;
			}, err => console.error(`Fail populating post`, err));
	}

	private createPost(): void {
		this.postService
			.create(this.savePost)
			.subscribe(r => {
				console.log(`Post created.`);
				this.populatePosts();
			}, err => console.error(`Fail to create post`, err));
	}
}
