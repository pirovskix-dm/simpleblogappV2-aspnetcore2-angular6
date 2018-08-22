import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AbstractControlOptions} from '@angular/forms/src/model';
import {ValidatorFn} from '@angular/forms/src/directives/validators';

export interface PostModel {
	id: number;
	title: string | null;
	content: string | null;
	shortContent: string | null;
	dateCreated: string | null;
	dateLastUpdated: string | null;
}

export class PostViewModel {

	private defaultPost: PostModel = {
		id: 0,
		title: ``,
		shortContent: ``,
		content: ``,
		dateCreated: ``,
		dateLastUpdated: ``
	};
	private model: any;
	private form: FormGroup;

	constructor(
	) {
		this.model = this.defaultPost as any;
		this.form = new FormGroup({});
		this.createControl(`title`, [Validators.required, Validators.maxLength(100)]);
		this.createControl(`shortContent`, Validators.maxLength(500));
		this.createControl(`content`, Validators.required);
	}

	public get(field: string): any {
		return this.model[field];
	}

	public set(field: string, value: any): void {
		this.model[field] = value;

		if (this.form.contains(field))
			this.form.controls[field].setValue(value);
	}

	get Form(): FormGroup {
		return this.form;
	}

	get Model(): PostModel {
		return this.model as PostModel;
	}
	set Model(value: PostModel) {
		this.model = value as any;
		this.form.patchValue(value);
	}

	public validate(field: string): boolean {
		return (this.form.controls[field].errors || false) && !this.form.controls[field].pristine;
	}

	private createControl(field: string, validator?: ValidatorFn | ValidatorFn[] | null): void {
		const control = new FormControl(this.model[field], validator);
		control.valueChanges.subscribe((v: string) => this.model[field] = v);
		this.form.addControl(field, control);
	}
}

