import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CategoryModel} from '../models/category-view-model';

@Injectable({providedIn: 'root'})
export class CategoryService {

	private readonly categoryEndpoint = '/api/categories';

	constructor(private http: HttpClient) {

	}

	public getCategoriesAsync(): Promise<CategoryModel[]> {
		return this
			.http
			.get<CategoryModel[]>(`${this.categoryEndpoint}/all`)
			.toPromise();
	}

	public getCategoryAsync(id: number): Promise<CategoryModel> {
		return this
			.http
			.get<CategoryModel>(`${this.categoryEndpoint}/${id}`)
			.toPromise();
	}

	public createAsync(post: CategoryModel): Promise<number> {
		return this
			.http
			.post<number>(this.categoryEndpoint, post)
			.toPromise();
	}

	public deleteAsync(id: number): Promise<number> {
		return this
			.http
			.delete<number>(`${this.categoryEndpoint}/${id}`)
			.toPromise();
	}

	public updateAsync(id: number, post: CategoryModel): Promise<number> {
		return this
			.http
			.put<number>(`${this.categoryEndpoint}/${id}`, post)
			.toPromise();
	}
}
