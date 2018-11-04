import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CategoryModel} from '../models/category-view-model';

@Injectable({providedIn: 'root'})
export class CategoryService {

	private readonly categoryEndpoint = '/api/categories';

	constructor(private http: HttpClient) {

	}

	public getCategories(): Observable<CategoryModel[]> {
		return this.http.get<CategoryModel[]>(`${this.categoryEndpoint}/all`);
	}

	public getCategory(id: number): Observable<CategoryModel> {
		return this.http.get<CategoryModel>(`${this.categoryEndpoint}/${id}`);
	}

	public create(post: CategoryModel): Observable<number> {
		return this.http.post<number>(this.categoryEndpoint, post);
	}

	public delete(id: number): Observable<number> {
		return this.http.delete<number>(`${this.categoryEndpoint}/${id}`);
	}

	public update(id: number, post: CategoryModel): Observable<number> {
		return this.http.put<number>(`${this.categoryEndpoint}/${id}`, post);
	}

	public getCategoriesAsync(): Promise<CategoryModel[]> {
		return new Promise<CategoryModel[]>((resolve, reject) => {
			this.getCategories().subscribe( result => {
				resolve(result);
			}, err => {
				reject(err);
			});
		});
	}
}
