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

import {PostService} from './services/post.service';
import {CategoryService} from './services/category.service';

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
		BlogTextComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forRoot([
			{path: '', component: BlogComponent, pathMatch: 'full'},
			{path: 'home', redirectTo: ''},
			{path: 'admin', component: AdminComponent},
			{path: 'error', redirectTo: ''},
			{path: 'post/create', component: PostFormComponent},
			{path: 'post/edit/:id', component: PostFormComponent},
			{path: 'post/:id', component: PostViewComponent},
			{path: '**', redirectTo: ''}
		])
	],
	providers: [PostService, CategoryService],
	bootstrap: [AppComponent]
})
export class AppModule {
}
