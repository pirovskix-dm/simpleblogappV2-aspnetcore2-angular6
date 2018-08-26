export interface Query {
	search: string | null;
	searchBy: string[];
	sortBy: string | null;
	isSortAscending: boolean;
	page: number;
	pageSize: number;
}