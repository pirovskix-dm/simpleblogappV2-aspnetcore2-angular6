import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostViewModel, SavePostViewModel} from '../models/post-view-model';
import {map} from 'rxjs/internal/operators';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	getPosts(): Observable<PostViewModel[]> {
		return this.http
			.get(this.postEndpoint)
			.pipe(
				map((res: any) => res as PostViewModel[])
			);
	}

	getPost(id: number): Observable<PostViewModel> {
		return this.http
			.get(`${this.postEndpoint}/${id}`)
			.pipe(
				map((res: any) => res as PostViewModel)
			);
	}

	create(savePost: SavePostViewModel): Observable<any> {
		return this.http
			.post(this.postEndpoint, savePost);
	}
}
