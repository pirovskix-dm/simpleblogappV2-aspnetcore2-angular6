export interface Query {
	search: string;
	searchBy: string[];
	sortBy: string;
	isSortAscending: boolean;
	page: number;
	pageSize: number;
}
