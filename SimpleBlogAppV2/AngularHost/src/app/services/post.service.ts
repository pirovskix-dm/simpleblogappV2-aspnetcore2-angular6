import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostModel} from '../models/post-view-model';
import {Query} from '../models/query';
import {QueryResultPost} from '../models/query-result-post';
import {CategoryModel} from '../models/category-view-model';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	public getPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(this.postEndpoint);
	}

	public getAdminPosts(query: Query): Observable<QueryResultPost> {
		return this.http.get<QueryResultPost>(`${this.postEndpoint}/admin?${this.toQueryString(query)}`);
	}

	public getBlogPosts(query: Query): Observable<QueryResultPost> {
		return this.http.get<QueryResultPost>(`${this.postEndpoint}/blog?${this.toQueryString(query)}`);
	}

	public getPost(id: number): Observable<PostModel> {
		return this.http.get<PostModel>(`${this.postEndpoint}/${id}`);
	}

	public getDefaultPost(): Observable<PostModel> {
		return this.http.get<PostModel>(`${this.postEndpoint}/default`);
	}

	public create(post: PostModel): Observable<number> {
		return this.http.post<number>(this.postEndpoint, post);
	}

	public delete(id: number): Observable<number> {
		return this.http.delete<number>(`${this.postEndpoint}/${id}`);
	}

	public update(id: number, post: PostModel): Observable<number> {
		return this.http.put<number>(`${this.postEndpoint}/${id}`, post);
	}

	public getPostAsync(id: number): Promise<PostModel> {
		return new Promise<PostModel>((resolve, reject) => {
			this.getPost(id).subscribe(result => {
				resolve(result);
			}, err => {
				reject(err);
			});
		});
	}

	private toQueryString(query: Query) {
		const parts: string[] = [];

		if (query.search) {
			parts.push(encodeURIComponent(`search`) + `=` + encodeURIComponent(query.search));
			parts.push(encodeURIComponent(`searchBy`) + `=` + encodeURIComponent(query.searchBy.toString()));
		}

		if (query.filters && query.filters.length > 0) {
			parts.push(encodeURIComponent(`filters`) + `=` + encodeURIComponent(query.filters.toString()));
		}

		if (query.sortBy) {
			parts.push(encodeURIComponent(`sortBy`) + `=` + encodeURIComponent(query.sortBy));
			parts.push(encodeURIComponent(`isSortAscending`) + `=` + encodeURIComponent(query.isSortAscending.toString()));
		}

		parts.push(encodeURIComponent(`page`) + `=` + encodeURIComponent(query.page.toString()));
		parts.push(encodeURIComponent(`pageSize`) + `=` + encodeURIComponent(query.pageSize.toString()));

		return parts.join('&');
	}
}
