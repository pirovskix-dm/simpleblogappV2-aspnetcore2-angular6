import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PostModel} from '../models/post-view-model';
import {Query} from '../models/query';
import {QueryResultPost} from '../models/query-result-post';
import {CategoryModel} from '../models/category-view-model';
import '../utils/extensions';
import * as _ from 'underscore';

@Injectable({providedIn: 'root'})
export class PostService {

	private readonly postEndpoint = '/api/posts';

	constructor(private http: HttpClient) {

	}

	public getPosts(): Observable<PostModel[]> {
		return this.http.get<PostModel[]>(this.postEndpoint);
	}

	public getAdminPostsAsync(query: Query): Promise<QueryResultPost> {
		return this
			.http
			.get<QueryResultPost>(`${this.postEndpoint}/admin`, {params: this.getParams(query)})
			.toPromise();
	}

	public getBlogPosts(query: Query): Observable<QueryResultPost> {
		return this.http.get<QueryResultPost>(`${this.postEndpoint}/blog`, {params: this.getParams(query)});
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
		return this.getPost(id).toPromise();
	}

	private getParams(query: Query): HttpParams {
		let params = new HttpParams();

		if (_.isUndefined(query) || _.isNull(query))
			return params;

		if (!query.search.isBlank()) {
			params = params.append(`search`, query.search);
			params = params.append(`searchBy`, query.searchBy.toString());
		}

		if (query.filters && query.filters.length > 0) {
			params = params.append(`filters`, query.filters.toString());
		}

		if (!query.sortBy.isBlank()) {
			params = params.append(`sortBy`, query.sortBy);
			params = params.append(`isSortAscending`, query.isSortAscending.toString());
		}

		params = params.append(`page`, query.page.toString());
		params = params.append(`pageSize`, query.pageSize.toString());

		return params;
	}
}
