import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { NzModalRef } from "ng-zorro-antd/modal";
import { NoWhitespaceValidator } from "src/app/validators/no-whitespace.validator";
import { CountryDto } from "src/app/web-api-client";
import * as moment from "moment-timezone";
import { selectLoadingSaveCountry } from "src/app/store/location/location.selectors";
import { saveCountry } from "src/app/store/location/location.actions";

@Component({
  selector: "app-add-edit-country-modal",
  templateUrl: "./add-edit-country-modal.component.html",
  styleUrls: ["./add-edit-country-modal.component.scss"],
})
export class AddEditCountryModalComponent implements OnInit {
  @Input() country: CountryDto;
  editForm: FormGroup;
  debug = false;
  timezones = [];
  loadingSaveCountry$ = this.store.select(selectLoadingSaveCountry);
  constructor(
    private modal: NzModalRef,
    private fb: FormBuilder,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.editForm = this.fb.group({
      id: this.country.id,
      name: [this.country.name, Validators.compose([NoWhitespaceValidator()])],
      languageCode: [
        this.country.languageCode
      ],
      priority: [
        this.country.priority,
        Validators.compose([Validators.required]),
      ],
      iconUrl: this.country.iconUrl,
      userDefined1: this.country.userDefined1,
      userDefined2: this.country.userDefined2,
      userDefined3: this.country.userDefined3,
    });
  }
  onSubmit() {
    this.store.dispatch(saveCountry({ country: this.editForm.value }));
  }
  destroyModal() {
    this.modal.destroy();
  }
  onChange(value: string): void {
    this.timezones = moment.tz
      .names()
      .filter(
        (option) => option.toLowerCase().indexOf(value.toLowerCase()) !== -1
      );
  }
}
