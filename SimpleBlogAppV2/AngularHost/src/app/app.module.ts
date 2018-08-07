import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {CounterComponent} from './components/counter/counter.component';
import {FetchDataComponent} from './components/fetch-data/fetch-data.component';
import {BlogComponent} from './components/blog/blog.component';

import {PostService} from './services/post.service';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		CounterComponent,
		HomeComponent,
		FetchDataComponent,
		BlogComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{path: '', component: HomeComponent, pathMatch: 'full'},
			{path: 'blog', component: BlogComponent},
			{path: 'counter', component: CounterComponent},
			{path: 'fetch-data', component: FetchDataComponent},
		])
	],
	providers: [PostService],
	bootstrap: [AppComponent]
})
export class AppModule {
}
