import { Action, createReducer, on } from "@ngrx/store";
import { CountryDto, ProvinceDto } from "src/app/web-api-client";
import {
  deleteCountry,
  loadCountries_Admin,
  loadCountries_Failure_Admin,
  loadCountries_Failure_Homepage,
  loadCountries_Hompage,
  loadCountries_Success_Admin,
  loadCountries_Success_Homepage,
  saveCountry,
  saveCountryFailure,
  saveCountrySuccess,
  saveProvince,
  saveProvinceFailure,
  saveProvinceSuccess,
  selectCountry_Homepage,
  selectProvince_Homepage,
} from "./location.actions";

export const locationFeatureKey = "location";

export interface LocationState {
  countriesList_Homepage: CountryDto[];
  loadingCountriesList_Homepage: boolean;
  selectedCountry_Homepage: CountryDto | null;
  selectedProvince_Homepage: ProvinceDto | null;
  countriesList_Admin: CountryDto[];
  loadingCountriesList_Admin: boolean;
  loadingSaveCountry: boolean;
  loadingSaveProvince: boolean;
}

export const initialLocationState: LocationState = {
  countriesList_Homepage: [],
  loadingCountriesList_Homepage: false,
  selectedCountry_Homepage: null,
  selectedProvince_Homepage: null,
  countriesList_Admin: [],
  loadingCountriesList_Admin: false,
  loadingSaveCountry: false,
  loadingSaveProvince: false,
};

export const locationReducer = createReducer(
  initialLocationState,
  on(loadCountries_Hompage, (state) => {
    return {
      ...state,
      loadingCountriesList_Homepage: true,
      selectedCountry_Homepage: null,
      countriesList_Homepage: [],
    };
  }),
  on(loadCountries_Success_Homepage, (state, { countriesList }) => {
    return {
      ...state,
      loadingCountriesList_Homepage: false,
      countriesList_Homepage: countriesList,
    };
  }),
  on(loadCountries_Failure_Homepage, (state) => {
    return {
      ...state,
      loadingCountriesList_Homepage: false,
      countriesList_Homepage: [],
    };
  }),
  on(selectCountry_Homepage, (state, { selectedCountry }) => {
    return { ...state, selectedCountry_Homepage: selectedCountry };
  }),
  on(selectProvince_Homepage, (state, { selectedProvince }) => {
    return { ...state, selectedProvince_Homepage: selectedProvince };
  }),
  on(loadCountries_Admin, (state) => {
    return {
      ...state,
      loadingCountriesList_Admin: true,
      countriesList_Admin: [],
    };
  }),
  on(loadCountries_Success_Admin, (state, { countriesList }) => {
    return {
      ...state,
      loadingCountriesList_Admin: false,
      countriesList_Admin: countriesList,
    };
  }),
  on(loadCountries_Failure_Admin, (state) => {
    return {
      ...state,
      loadingCountriesList_Admin: false,
      countriesList_Admin: [],
    };
  }),
  on(saveCountry, (state) => {
    return {
      ...state,
      loadingSaveCountry: true,
    };
  }),
  on(saveCountrySuccess, (state) => {
    return {
      ...state,
      loadingSaveCountry: false,
    };
  }),
  on(saveCountryFailure, (state) => {
    return {
      ...state,
      loadingSaveCountry: false,
    };
  }),
  on(saveProvince, (state) => {
    return {
      ...state,
      loadingSaveProvince: true,
    };
  }),
  on(saveProvinceSuccess, (state) => {
    return {
      ...state,
      loadingSaveProvince: false,
    };
  }),
  on(saveProvinceFailure, (state) => {
    return {
      ...state,
      loadingSaveProvince: false,
    };
  })
);
