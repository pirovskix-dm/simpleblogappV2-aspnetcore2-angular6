import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {BlogComponent} from './components/blog/blog.component';
import {AdminComponent} from './components/admin/admin.component';
import {PostFormComponent} from './components/post-form/post-form.component';
import {PostViewComponent} from './components/post-view/post-view.component';
import {PaginationComponent} from './components/pagination/pagination.component';
import {BlogInputComponent} from './components/extensions/blog-input/blog-input.component';
import {BlogTextareaComponent} from './components/extensions/blog-textarea/blog-textarea.component';
import {BlogTextComponent} from './components/extensions/blog-text/blog-text.component';
import {RegistrationFormComponent} from './components/registration-form/registration-form.component';
import {LoginFormComponent} from './components/login-form/login-form.component';

import {PostService} from './services/post.service';
import {CategoryService} from './services/category.service';
import {CookieService} from 'ngx-cookie-service';

import {AuthGuard} from './guards/auth.guard';
import {RoleGuard} from './guards/role.guard';

import {Roles} from './Constants/Roles';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		BlogComponent,
		AdminComponent,
		PostFormComponent,
		PostViewComponent,
		PaginationComponent,
		BlogInputComponent,
		BlogTextareaComponent,
		BlogTextComponent,
		RegistrationFormComponent,
		LoginFormComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forRoot([
			{path: '', component: BlogComponent, pathMatch: 'full'},
			{path: 'home', redirectTo: ''},
			{path: 'admin', component: AdminComponent, canActivate: [RoleGuard], data: { expectedRole: Roles.ADMIN}},
			{path: 'login', component: LoginFormComponent},
			{path: 'error', redirectTo: ''},
			{path: 'post/create', component: PostFormComponent, canActivate: [AuthGuard]},
			{path: 'post/edit/:id', component: PostFormComponent, canActivate: [AuthGuard]},
			{path: 'post/:id', component: PostViewComponent},
			{path: '**', redirectTo: ''}
		])
	],
	providers: [PostService, CategoryService, CookieService],
	bootstrap: [AppComponent]
})
export class AppModule {
}
