import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
	providedIn: 'root'
})
export class AccountService {

	private readonly accountEndpoint = '/api/accounts';
	private readonly cookieTokenName = `api_access`;
	private readonly cookiePath = `/`;

	constructor(private http: HttpClient, private cookieService: CookieService) {

	}

	public async login(userName: string, password: string): Promise<void> {
		this.logout();

		const token = await this.http
			.post<string>(`${this.accountEndpoint}/login`, { userName, password })
			.toPromise();

		if (token)
			this.cookieService.set(this.cookieTokenName, token, 1, this.cookiePath);
	}

	public isLoggedIn(): boolean {
		return !!this.cookieService.get(`auth_token`);
	}

	public logout(): void {
		if (this.cookieService.check(this.cookieTokenName))
			this.cookieService.delete(this.cookieTokenName, this.cookiePath);
	}
}
