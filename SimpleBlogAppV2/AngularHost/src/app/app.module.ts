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

import {PostService} from './services/post.service';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		BlogComponent,
		AdminComponent,
		PostFormComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forRoot([
			{path: '', component: BlogComponent, pathMatch: 'full'},
			{path: 'admin', component: AdminComponent},
			{path: 'post/create', component: PostFormComponent},
			{path: 'post/edit/:id', component: PostFormComponent},
			{path: 'error', redirectTo: ''},
			{path: 'home', redirectTo: ''},
			{path: '**', redirectTo: ''}
		])
	],
	providers: [PostService],
	bootstrap: [AppComponent]
})
export class AppModule {
}
