export interface Query {
	search: string;
	searchBy: string[];
	filters: string[];
	sortBy: string;
	isSortAscending: boolean;
	page: number;
	pageSize: number;
}
