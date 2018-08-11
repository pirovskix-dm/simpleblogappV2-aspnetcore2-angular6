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
		return this.http.get<PostViewModel[]>(this.postEndpoint);
	}

	getPost(id: number): Observable<PostViewModel> {
		return this.http.get<PostViewModel>(`${this.postEndpoint}/${id}`);
	}

	create(savePost: SavePostViewModel): Observable<any> {
		return this.http.post(this.postEndpoint, savePost);
	}
}
