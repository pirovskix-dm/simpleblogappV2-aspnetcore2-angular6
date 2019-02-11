import {Component} from '@angular/core';
import {AccountService} from '../../services/account.service';
import {Roles} from '../../Constants/Roles';

@Component({
	selector: 'app-nav-menu',
	templateUrl: './nav-menu.component.html',
	styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
	isExpanded = false;

	constructor(
		private accountService: AccountService
	) {
	}

	public logout(): void {
		this.accountService.logout();
		this.collapse();
	}

	public collapse(): void {
		this.isExpanded = false;
	}

	public toggle(): void {
		this.isExpanded = !this.isExpanded;
	}

	public isAdmin(): boolean {
		return this.accountService.getUserRole() === Roles.ADMIN;
	}

	public isLoggedIn(): boolean {
		return this.accountService.isLoggedIn();
	}
}
