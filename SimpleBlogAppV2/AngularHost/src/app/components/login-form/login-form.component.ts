import {Component, OnInit} from '@angular/core';
import {AccountService} from '../../services/account.service';
import {Router} from '@angular/router';
import {CookieService} from 'ngx-cookie-service';

@Component({
	selector: 'app-login-form',
	templateUrl: './login-form.component.html',
	styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

	public userName = ``;
	public password = ``;
	public errorMessage = ``;
	public successMessage = ``;

	constructor(
		private accountService: AccountService,
		private router: Router,
		private cookieService: CookieService
	) {
	}

	ngOnInit() {
		this.errorMessage = ``;
		this.successMessage = ``;
	}

	public async login(): Promise<void> {
		try {
			await this.accountService.login(this.userName, this.password);
			this.successMessage = `Login success.`;
		} catch (e) {
			this.errorMessage = e.error.message;
			console.error(e.error.message, e.error);
		}
	}
}
