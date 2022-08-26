import { createAction, props } from "@ngrx/store";
import { CountryDto, ProvinceDto } from "src/app/web-api-client";

export enum LocationActions {
  LOCATION_LOADCOUNTRIES_HOMEPAGE = "[Location] Load Countries Homepage",
  LOCATION_LOADCOUNTRIES_SUCCESS_HOMEPAGE = "[Location] Load Countries success Homepage",
  LOCATION_LOADCOUNTRIES_FAILURE_HOMPAGE = "[Location] Load Countries failure Homepage",
  LOCATION_SELECTCOUNTRY_HOMEPAGE = "[Location] Select Country on HomePage",
  LOCATION_SELECTPROVINCE_HOMEPAGE = "[Location] Select Province on HomePage",

  LOCATION_LOADCOUNTRIES_ADMIN = "[Location] Load Countries Admin",
  LOCATION_LOADCOUNTRIES_SUCCESS_ADMIN = "[Location] Load Countries success Admin",
  LOCATION_LOADCOUNTRIES_FAILURE_ADMIN = "[Location] Load Countries failure Admin",

  COUNTRY_SHOWMODAL_ADMIN = "[Location] Country Show Modal Admin",
  LOCATION_SAVECOUNTRY = "[Location] Save Country",
  LOCATION_SAVECOUNTRY_SUCCESS = "[Location] Save Country Success",
  LOCATION_SAVECOUNTRY_FAILURE = "[Location] Save Country Failure",
  LOCATION_DELETECOUNTRY = "[Location] Delete Country",
  LOCATION_DELETECOUNTRY_SUCCESS = "[Location] Delete Country Success",
  LOCATION_DELETECOUNTRY_FAILURE = "[Location] Delete Country Failure",

  PROVINCE_SHOWMODAL_ADMIN = "[Location] Province Show Modal Admin",
  LOCATION_SAVEPROVINCE = "[Location] Save Province",
  LOCATION_SAVEPROVINCE_SUCCESS = "[Location] Save Province Success",
  LOCATION_SAVEPROVINCE_FAILURE = "[Location] Save Province Failure",
  LOCATION_DELETEPROVINCE = "[Location] Delete Province",
  LOCATION_DELETEPROVINCE_SUCCESS = "[Location] Delete Province Success",
  LOCATION_DELETEPROVINCE_FAILURE = "[Location] Delete Province Failure",
}
export const loadCountries_Hompage = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_HOMEPAGE
);
export const loadCountries_Success_Homepage = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_SUCCESS_HOMEPAGE,
  props<{ countriesList: CountryDto[] }>()
);
export const loadCountries_Failure_Homepage = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_FAILURE_HOMPAGE,
  props<{ error: any }>()
);

export const selectCountry_Homepage = createAction(
  LocationActions.LOCATION_SELECTCOUNTRY_HOMEPAGE,
  props<{ selectedCountry: CountryDto }>()
);
export const selectProvince_Homepage = createAction(
  LocationActions.LOCATION_SELECTPROVINCE_HOMEPAGE,
  props<{ selectedProvince: ProvinceDto }>()
);

export const loadCountries_Admin = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_ADMIN
);
export const loadCountries_Success_Admin = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_SUCCESS_ADMIN,
  props<{ countriesList: CountryDto[] }>()
);
export const loadCountries_Failure_Admin = createAction(
  LocationActions.LOCATION_LOADCOUNTRIES_FAILURE_ADMIN,
  props<{ error: any }>()
);

export const showModalCountry_Admin = createAction(
  LocationActions.COUNTRY_SHOWMODAL_ADMIN,
  props<{ country: CountryDto }>()
);
export const saveCountry = createAction(
  LocationActions.LOCATION_SAVECOUNTRY,
  props<{ country: CountryDto }>()
);
export const saveCountrySuccess = createAction(
  LocationActions.LOCATION_SAVECOUNTRY_SUCCESS
);
export const saveCountryFailure = createAction(
  LocationActions.LOCATION_SAVECOUNTRY_FAILURE,
  props<{ error: any }>()
);

export const showModalProvince_Admin = createAction(
  LocationActions.PROVINCE_SHOWMODAL_ADMIN,
  props<{ province: ProvinceDto }>()
);
export const saveProvince = createAction(
  LocationActions.LOCATION_SAVEPROVINCE,
  props<{ province: ProvinceDto }>()
);
export const saveProvinceSuccess = createAction(
  LocationActions.LOCATION_SAVEPROVINCE_SUCCESS
);
export const saveProvinceFailure = createAction(
  LocationActions.LOCATION_SAVEPROVINCE_FAILURE,
  props<{ error: any }>()
);

export const deleteProvince = createAction(
  LocationActions.LOCATION_DELETEPROVINCE,
  props<{ provinceId: number }>()
);
export const deleteProvinceSuccess = createAction(
  LocationActions.LOCATION_DELETEPROVINCE_SUCCESS
);
export const deleteProvinceFailure = createAction(
  LocationActions.LOCATION_DELETEPROVINCE_FAILURE,
  props<{ error: any }>()
);

export const deleteCountry = createAction(
  LocationActions.LOCATION_DELETECOUNTRY,
  props<{ countryId: number }>()
);
export const deleteCountrySuccess = createAction(
  LocationActions.LOCATION_DELETECOUNTRY_SUCCESS
);
export const deleteCountryFailure = createAction(
  LocationActions.LOCATION_DELETECOUNTRY_FAILURE,
  props<{ error: any }>()
);
