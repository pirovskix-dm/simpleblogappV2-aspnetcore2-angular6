import * as _ from 'underscore';

declare global {
	interface String {
		isBlank(): boolean;
	}
}

String.prototype.isBlank = function(): boolean {
	const value = String(this);

	if (!value)
		return true;

	if (_.isUndefined(value))
		return true;

	if (_.isNull(value))
		return true;

	if (_.isEmpty(value.trim()))
		return true;

	if (!!value.match(/^\s*$/))
		return true;

	return false;
};


