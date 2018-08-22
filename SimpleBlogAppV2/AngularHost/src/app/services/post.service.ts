import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostModel} from '../models/post-view-model';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	getPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(this.postEndpoint);
	}

	getAdminPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(`${this.postEndpoint}/admin`);
	}

	getBlogPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(`${this.postEndpoint}/blog`);
	}

	getPost(id: number): Observable<PostModel> {
		return this.http.get<PostModel>(`${this.postEndpoint}/${id}`);
	}

	getDefaultPost(): Observable<PostModel> {
		return this.http.get<PostModel>(`${this.postEndpoint}/default`);
	}

	create(post: PostModel): Observable<number> {
		return this.http.post<number>(this.postEndpoint, post);
	}

	delete(id: number): Observable<number> {
		return this.http.delete<number>(`${this.postEndpoint}/${id}`);
	}

	update(id: number, post: PostModel): Observable<number> {
		return this.http.put<number>(`${this.postEndpoint}/${id}`, post);
	}
}
