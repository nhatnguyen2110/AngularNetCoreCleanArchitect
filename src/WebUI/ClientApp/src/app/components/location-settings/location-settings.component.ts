import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { NzModalService } from "ng-zorro-antd/modal";
import {
  deleteCountry,
  deleteProvince,
  loadCountries_Admin,
  showModalCountry_Admin,
  showModalProvince_Admin,
} from "src/app/store/location/location.actions";
import {
  selectCountriesList_Admin,
  selectLoadingCountriesList_Admin,
} from "src/app/store/location/location.selectors";
import { CountryDto, ProvinceDto } from "src/app/web-api-client";
import { AddEditCountryModalComponent } from "./add-edit-country-modal/add-edit-country-modal.component";

@Component({
  selector: "app-location-settings",
  templateUrl: "./location-settings.component.html",
  styleUrls: ["./location-settings.component.scss"],
})
export class LocationSettingsComponent implements OnInit {
  listOfCountries: CountryDto[] = [];
  listOfProvinces: ProvinceDto[] = [];
  expandSet = new Set<number>();
  loadingCountriesList$ = this.store.select(selectLoadingCountriesList_Admin);
  countriesList$ = this.store.select(selectCountriesList_Admin);
  constructor(private store: Store, private modal: NzModalService) {}

  ngOnInit(): void {
    this.store.dispatch(loadCountries_Admin());
    for (let i = 0; i < 3; ++i) {
      this.listOfCountries.push(
        CountryDto.fromJS({
          id: i,
          name: "Country " + i,
          priority: i,
          languageCode: "code " + i,
          userDefined1: "userdefined1",
          userDefined2: "flag-icon flag-icon-vn",
          userDefined3: "Asia/Ho_Chi_Minh",
        })
      );
    }
    for (let i = 0; i < 3; ++i) {
      this.listOfProvinces.push(
        ProvinceDto.fromJS({
          id: i,
          name: "Province " + i,
          longitude: i,
          latitude: i,
        })
      );
    }
  }

  onExpandChange(id: number, checked: boolean): void {
    if (checked) {
      this.expandSet.add(id);
    } else {
      this.expandSet.delete(id);
    }
  }
  onAddCountry() {
    let country = CountryDto.fromJS({
      id: 0,
      name: null,
      priority: 0,
      languageCode: null,
      iconUrl: null,
      userDefined1: null,
      userDefined2: null,
      userDefined3: null,
    });
    this.store.dispatch(showModalCountry_Admin({ country }));
  }
  onEditCountry(country: CountryDto) {
    this.store.dispatch(showModalCountry_Admin({ country }));
  }
  onDeleteCountry(countryId: number) {
    this.store.dispatch(deleteCountry({ countryId }));
  }
  onAddProvince(countryId: number) {
    let province = ProvinceDto.fromJS({
      id: 0,
      countryId: countryId,
      name: null,
      longitude: null,
      latitude: null,
    });
    this.store.dispatch(showModalProvince_Admin({ province }));
  }
  onEditProvince(province: ProvinceDto) {
    this.store.dispatch(showModalProvince_Admin({ province }));
  }
  onDeleteProvince(provinceId: number) {
    this.store.dispatch(deleteProvince({ provinceId }));
  }
}
