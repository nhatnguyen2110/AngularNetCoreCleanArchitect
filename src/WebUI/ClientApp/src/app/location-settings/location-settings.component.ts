import { Component, OnInit, TemplateRef } from "@angular/core";
import {
  CountriesClient,
  CountryDto,
  CreateCountryCommand,
  CreateProvinceCommand,
  ProvinceDto,
  ProvincesClient,
  UpdateCountryCommand,
  UpdateProvinceCommand,
} from "../web-api-client";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { faPlus, faEllipsisH } from "@fortawesome/free-solid-svg-icons";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NoWhitespaceValidator } from "../validators/no-whitespace.validator";
import { isNgTemplate } from "@angular/compiler";
import { NotifierService } from "angular-notifier";

@Component({
  selector: "app-location-settings",
  templateUrl: "./location-settings.component.html",
  styleUrls: ["./location-settings.component.scss"],
})
export class LocationSettingsComponent implements OnInit {
  countriesList?: CountryDto[];
  selectedCountry: CountryDto;
  selectedProvince: ProvinceDto;

  newCountryForm: FormGroup;
  editProvinceForm: FormGroup;

  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  debug = false;

  constructor(
    private countriesClient: CountriesClient,
    private provincesClient: ProvincesClient,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private notifier: NotifierService
  ) {
    countriesClient.getList(null, 1, 99).subscribe(
      (result) => {
        this.countriesList = result.data.items;
        if (this.countriesList.length) {
          this.selectedCountry = this.countriesList[0];
        }
      },
      (error) => console.error(error)
    );
  }
  ngOnInit(): void {
    this.newCountryForm = this.fb.group({
      id: 0,
      name: ["", Validators.compose([NoWhitespaceValidator()])],
      languageCode: ["", Validators.compose([NoWhitespaceValidator()])],
      priority: [0, Validators.compose([Validators.required])],
      iconUrl: "",
      userDefined1: "",
      userDefined2: "",
      userDefined3: "",
    });
    this.editProvinceForm = this.fb.group({
      id: 0,
      name: ["", Validators.compose([NoWhitespaceValidator()])],
      priority: [0, Validators.compose([Validators.required])],
      aliasName: "",
      longitude: null,
      latitude: null,
      countryId: [null, Validators.compose([Validators.required])],
    });
  }

  remainingItems(list: CountryDto): number {
    if (list.provinces) return list.provinces.length;
    return 0;
  }

  showNewListModal(template: TemplateRef<any>): void {
    this.newCountryForm.reset();
    this.newListModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("countryName").focus(), 250);
  }

  newListCancelled(): void {
    this.newListModalRef.hide();
  }

  addList(): void {
    let country = CountryDto.fromJS(this.newCountryForm.value);
    if (country.id && country.id > 0) {
      this.countriesClient
        .update(UpdateCountryCommand.fromJS(this.newCountryForm.value))
        .subscribe(
          (result) => {
            this.selectedCountry.name = country.name;
            this.selectedCountry.languageCode = country.languageCode;
            this.selectedCountry.priority = country.priority;
            this.selectedCountry.userDefined1 = country.userDefined1;
            this.selectedCountry.userDefined2 = country.userDefined2;
            this.selectedCountry.userDefined3 = country.userDefined3;

            this.newListModalRef.hide();
          },
          (error) => {
            let errors = JSON.parse(error.response);
            this.notifier.notify("error", errors.message);
            setTimeout(
              () => document.getElementById("countryName").focus(),
              250
            );
          }
        );
    } else {
      this.countriesClient
        .create(CreateCountryCommand.fromJS(this.newCountryForm.value))
        .subscribe(
          (result) => {
            country.id = result.data;
            country.provinces = [];
            this.countriesList.push(country);
            this.selectedCountry = country;
            this.newListModalRef.hide();
          },
          (error) => {
            let errors = JSON.parse(error.response);
            this.notifier.notify("error", errors.message);
            setTimeout(
              () => document.getElementById("countryName").focus(),
              250
            );
          }
        );
    }
  }

  showListOptionsModal(template: TemplateRef<any>) {
    if (this.selectedCountry) {
      this.newCountryForm.reset();
      this.newCountryForm.setValue({
        id: this.selectedCountry.id,
        name: this.selectedCountry.name,
        languageCode: this.selectedCountry.languageCode,
        priority: this.selectedCountry.priority,
        iconUrl: this.selectedCountry.iconUrl,
        userDefined1: this.selectedCountry.userDefined1,
        userDefined2: this.selectedCountry.userDefined2,
        userDefined3: this.selectedCountry.userDefined3,
      });
      this.newListModalRef = this.modalService.show(template);
      setTimeout(() => document.getElementById("countryName").focus(), 250);
    }
  }

  updateListOptions() {}

  confirmDeleteList(template: TemplateRef<any>) {
    this.newListModalRef.hide();
    this.deleteListModalRef = this.modalService.show(template);
  }

  deleteListConfirmed(): void {
    this.countriesClient.delete(this.selectedCountry.id).subscribe(
      () => {
        this.deleteListModalRef.hide();
        this.countriesList = this.countriesList.filter(
          (t) => t.id != this.selectedCountry.id
        );
        this.selectedCountry = this.countriesList.length
          ? this.countriesList[0]
          : null;
      },
      (error) => {
        let errors = JSON.parse(error.response);
        this.notifier.notify("error", errors.message);
      }
    );
  }

  // Provinces

  showItemDetailsModal(template: TemplateRef<any>, item: ProvinceDto): void {
    this.selectedProvince = item;
    this.editProvinceForm.setValue({
      id: item.id,
      countryId: item.countryId,
      name: item.name,
      priority: item.priority,
      aliasName: item.aliasName,
      longitude: item.longitude,
      latitude: item.latitude,
    });
    this.itemDetailsModalRef = this.modalService.show(template);
  }

  updateItemDetails(): void {
    this.provincesClient
      .update(UpdateProvinceCommand.fromJS({ ...this.editProvinceForm.value }))
      .subscribe(
        () => {
          if (
            this.selectedProvince.countryId !=
            this.editProvinceForm.value.countryId
          ) {
            this.selectedCountry.provinces =
              this.selectedCountry.provinces.filter(
                (i) => i.id != this.selectedProvince.id
              );
            let listIndex = this.countriesList.findIndex(
              (l) => l.id == this.editProvinceForm.value.countryId
            );
            this.selectedProvince.countryId =
              this.editProvinceForm.value.countryId;
            this.countriesList[listIndex].provinces.push(this.selectedProvince);
          }

          this.selectedProvince.priority = this.editProvinceForm.value.priority;
          this.selectedProvince.name = this.editProvinceForm.value.name;
          this.selectedProvince.aliasName =
            this.editProvinceForm.value.aliasName;
          this.selectedProvince.longitude =
            this.editProvinceForm.value.longitude;
          this.selectedProvince.latitude = this.editProvinceForm.value.latitude;
          this.itemDetailsModalRef.hide();
        },
        (error) => console.error(error)
      );
  }

  addItem() {
    let province = ProvinceDto.fromJS({
      id: 0,
      countryId: this.selectedCountry.id,
      priority: 0,
      name: "",
      aliasName: "",
      longitude: null,
      latitude: null,
    });

    this.selectedCountry.provinces.push(province);
    let index = this.selectedCountry.provinces.length - 1;
    this.editItem(province, "itemTitle" + index);
  }

  editItem(item: ProvinceDto, inputId: string): void {
    this.selectedProvince = item;
    setTimeout(() => document.getElementById(inputId).focus(), 100);
  }

  updateItem(item: ProvinceDto, pressedEnter: boolean = false): void {
    let isNewItem = item.id == 0;

    if (!item.name.trim()) {
      this.deleteItem(item);
      return;
    }

    if (item.id == 0) {
      this.provincesClient.create(CreateProvinceCommand.fromJS(item)).subscribe(
        (result) => {
          item.id = result.data;
        },
        (error) => console.error(error)
      );
    } else {
      this.provincesClient.update(UpdateProvinceCommand.fromJS(item)).subscribe(
        () => console.log("Update succeeded."),
        (error) => console.error(error)
      );
    }

    this.selectedProvince = null;

    if (isNewItem && pressedEnter) {
      this.addItem();
    }
  }

  // Delete item
  deleteItem(item: ProvinceDto) {
    if (this.itemDetailsModalRef) {
      this.itemDetailsModalRef.hide();
    }

    if (item.id == 0) {
      let itemIndex = this.selectedCountry.provinces.indexOf(
        this.selectedProvince
      );
      this.selectedCountry.provinces.splice(itemIndex, 1);
    } else {
      this.provincesClient.delete(item.id).subscribe(
        () =>
          (this.selectedCountry.provinces =
            this.selectedCountry.provinces.filter((t) => t.id != item.id)),
        (error) => {
          let errors = JSON.parse(error.response);
          this.notifier.notify("error", errors.message);
        }
      );
    }
  }
  log(event: any) {
    console.log(event);
  }
}
