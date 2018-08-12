import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostViewModel} from '../models/post-view-model';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	getPosts(): Observable<PostViewModel[]> {
		return this.http.get<PostViewModel[]>(this.postEndpoint);
	}

	getAdminPosts(): Observable<PostViewModel[]> {
		return this.http.get<PostViewModel[]>(`${this.postEndpoint}/admin`);
	}

	getBlogPosts(): Observable<PostViewModel[]> {
		return this.http.get<PostViewModel[]>(`${this.postEndpoint}/blog`);
	}

	getPost(id: number): Observable<PostViewModel> {
		return this.http.get<PostViewModel>(`${this.postEndpoint}/${id}`);
	}

	getDefaultPost(): Observable<PostViewModel> {
		return this.http.get<PostViewModel>(`${this.postEndpoint}/default`);
	}

	create(post: PostViewModel): Observable<number> {
		return this.http.post<number>(this.postEndpoint, post);
	}

	delete(id: number): Observable<number> {
		return this.http.delete<number>(`${this.postEndpoint}/${id}`);
	}

	update(id: number, post: PostViewModel): Observable<number> {
		return this.http.put<number>(`${this.postEndpoint}/${id}`, post);
	}
}
