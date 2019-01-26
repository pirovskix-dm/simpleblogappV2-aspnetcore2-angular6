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

	public getPostsAsync(): Promise<PostModel[]> {
		return this
			.http
			.get<PostModel[]>(this.postEndpoint)
			.toPromise();
	}

	public getAdminPostsAsync(query: Query): Promise<QueryResultPost> {
		return this
			.http
			.get<QueryResultPost>(`${this.postEndpoint}/admin`, {params: this.getParams(query)})
			.toPromise();
	}

	public getBlogPostsAsync(query: Query): Promise<QueryResultPost> {
		return this
			.http
			.get<QueryResultPost>(`${this.postEndpoint}/blog`, {params: this.getParams(query)})
			.toPromise();
	}

	public getPostAsync(id: number): Promise<PostModel> {
		return this
			.http
			.get<PostModel>(`${this.postEndpoint}/${id}`)
			.toPromise();
	}

	public getDefaultPostAsync(): Promise<PostModel> {
		return this
			.http
			.get<PostModel>(`${this.postEndpoint}/default`)
			.toPromise();
	}

	public createAsync(post: PostModel): Promise<number> {
		return this
			.http
			.post<number>(this.postEndpoint, post)
			.toPromise();
	}

	public deleteAsync(id: number): Promise<number> {
		return this
			.http
			.delete<number>(`${this.postEndpoint}/${id}`)
			.toPromise();
	}

	public updateAsync(id: number, post: PostModel): Promise<number> {
		return this
			.http
			.put<number>(`${this.postEndpoint}/${id}`, post)
			.toPromise();
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
