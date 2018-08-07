export interface PostViewModel {
	id: number;
	title: string;
	content: string;
	shortContent: string;
	dateCreated: string | null;
	dateLastUpdated: string | null;
}

export interface SavePostViewModel {
	title: string;
	content: string;
	shortContent: string;
}