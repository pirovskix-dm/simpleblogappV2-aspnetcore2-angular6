import {PostModel} from './post-view-model';

export interface QueryResultPost {
	totalItems: number;
	items: PostModel[];
}

