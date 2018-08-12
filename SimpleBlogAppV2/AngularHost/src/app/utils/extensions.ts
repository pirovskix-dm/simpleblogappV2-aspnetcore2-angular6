interface Date {
	toDDMMYYYY(separator: string): string;
}

interface String {
	toDDMMYYYY(separator: string): string;
}

Date.prototype.toDDMMYYYY = function(separator: string): string {
	const d: Date = this;
	let day: string = d.getDate().toString();
	let month: string = (d.getMonth() + 1).toString();
	const year: string = d.getFullYear().toString();
	day = day.length < 2 ? `0` + day : day;
	month = month.length < 2 ? `0` + month : month;
	return [ day, month, year ].join(separator);
};

String.prototype.toDDMMYYYY = function(separator: string): string {
	const date: number = Date.parse(this.toString());
	if (!date) {
		return '';
	}
	return new Date(date).toDDMMYYYY(separator);
};


