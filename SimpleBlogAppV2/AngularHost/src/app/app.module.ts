import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {BlogComponent} from './components/blog/blog.component';
import {AdminComponent} from './components/admin/admin.component';

import {PostService} from './services/post.service';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		BlogComponent,
		AdminComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{path: '', component: HomeComponent, pathMatch: 'full'},
			{path: 'blog', component: BlogComponent},
			{path: 'admin', component: AdminComponent}
		])
	],
	providers: [PostService],
	bootstrap: [AppComponent]
})
export class AppModule {
}
