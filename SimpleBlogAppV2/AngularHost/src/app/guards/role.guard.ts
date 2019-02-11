import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AccountService} from '../services/account.service';

@Injectable({
	providedIn: 'root'
})
export class RoleGuard implements CanActivate {
	constructor(private router: Router, private accountService: AccountService) {

	}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
		: Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

		const expectedRole = route.data.expectedRole;
		const role = this.accountService.getUserRole();

		if (!this.accountService.isLoggedIn() || role !== expectedRole) {
			this.router.navigate([`/login`]);
			return false;
		}

		return true;
	}
}
