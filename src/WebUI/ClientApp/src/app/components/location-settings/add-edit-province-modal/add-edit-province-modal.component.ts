import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { NzModalRef } from "ng-zorro-antd/modal";
import { saveProvince } from "src/app/store/location/location.actions";
import { selectLoadingSaveProvince } from "src/app/store/location/location.selectors";
import { NoWhitespaceValidator } from "src/app/validators/no-whitespace.validator";
import { ProvinceDto } from "src/app/web-api-client";

@Component({
  selector: "app-add-edit-province-modal",
  templateUrl: "./add-edit-province-modal.component.html",
  styleUrls: ["./add-edit-province-modal.component.scss"],
})
export class AddEditProvinceModalComponent implements OnInit {
  @Input() province: ProvinceDto;
  editForm: FormGroup;
  debug = false;
  loadingSaveProvince$ = this.store.select(selectLoadingSaveProvince);
  constructor(
    private store: Store,
    private modal: NzModalRef,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.editForm = this.fb.group({
      id: this.province.id,
      countryId: this.province.countryId,
      name: [this.province.name, Validators.compose([NoWhitespaceValidator()])],
      longitude: this.province.longitude,
      latitude: this.province.latitude,
    });

  }
  onSubmit() {
    this.store.dispatch(saveProvince({ province: this.editForm.value }));
  }
  destroyModal() {
    this.modal.destroy();
  }
}
