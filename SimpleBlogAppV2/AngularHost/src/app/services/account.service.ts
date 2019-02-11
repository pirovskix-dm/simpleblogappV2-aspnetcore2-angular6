import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {CookieService} from 'ngx-cookie-service';
import {Roles} from '../Constants/Roles';

@Injectable({
	providedIn: 'root'
})
export class AccountService {

	private readonly accountEndpoint = '/api/accounts';
	private readonly cookieTokenName = `simpleBlogApp_auth_token`;
	private readonly cookieRoleName = `simpleBlogApp_role`;
	private readonly cookiePath = `/`;

	constructor(private http: HttpClient, private cookieService: CookieService) {

	}

	public async login(userName: string, password: string): Promise<void> {
		this.logout();

		const token = await this.http
			.post<string>(`${this.accountEndpoint}/login`, { userName, password })
			.toPromise();

		if (token) {
			this.cookieService.set(this.cookieTokenName, token, 1, this.cookiePath);
			this.cookieService.set(this.cookieRoleName, Roles.ADMIN, 1, this.cookiePath);
		}
	}

	public isLoggedIn(): boolean {
		return !!this.cookieService.get(this.cookieTokenName);
	}

	public getUserRole(): string {
		if (!this.isLoggedIn())
			return Roles.ANONYMOUS;

		const currRule = this.cookieService.get(this.cookieRoleName);
		return !!currRule ? currRule : Roles.ANONYMOUS;
	}

	public logout(): void {
		if (this.cookieService.check(this.cookieTokenName))
			this.cookieService.delete(this.cookieTokenName, this.cookiePath);

		if (this.cookieService.check(this.cookieRoleName))
			this.cookieService.delete(this.cookieRoleName, this.cookiePath);
	}
}
