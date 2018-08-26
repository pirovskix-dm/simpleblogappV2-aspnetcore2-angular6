import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostModel} from '../models/post-view-model';
import {Query} from '../models/query';
import {QueryResultPost} from '../models/query-result-post';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	getPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(this.postEndpoint);
	}

	getAdminPosts(query: Query): Observable<QueryResultPost> {
		return this.http.get<QueryResultPost>(`${this.postEndpoint}/admin?${this.toQueryString(query)}`);
	}

	getBlogPosts(query: Query): Observable<QueryResultPost> {
		return this.http.get<QueryResultPost>(`${this.postEndpoint}/blog?${this.toQueryString(query)}`);
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

	private toQueryString(obj: any) {
		const parts: string[] = [];
		Object.keys(obj).forEach(prop => {
			const val = obj[prop];
			if (val !== null && val !== undefined)
				parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(val));
		});
		return parts.join('&');
	}
}
