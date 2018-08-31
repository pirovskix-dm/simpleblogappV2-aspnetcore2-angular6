export interface Query {
	search: string;
	searchBy: string[];
	sortBy: string | null;
	isSortAscending: boolean;
	page: number;
	pageSize: number;
}