import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {AccountService} from '../services/account.service';
import {Observable} from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class AuthGuard implements CanActivate {
	constructor(private router: Router, private accountService: AccountService) {

	}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
		: Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return true;
		if (!this.accountService.isLoggedIn()) {
			this.router.navigate([`/login`]);
			return false;
		}
		return true;
	}
}
