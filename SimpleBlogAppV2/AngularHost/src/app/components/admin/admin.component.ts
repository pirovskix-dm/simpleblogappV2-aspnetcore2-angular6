import {Component, OnInit} from '@angular/core';
import {PostViewModel} from '../../models/post-view-model';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
	posts: PostViewModel[] = [];

	constructor() {

	}

	ngOnInit() {

	}

}
