import { createFeatureSelector, createSelector } from "@ngrx/store";
import { locationFeatureKey, LocationState } from "./location.reducer";

export const selectLocationState =
  createFeatureSelector<LocationState>(locationFeatureKey);
export const selectLoadingCountriesList_Homepage = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.loadingCountriesList_Homepage;
    }
  }
);
export const selectCountriesList_Homepage = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.countriesList_Homepage;
    }
  }
);
export const selectSelectedCountry_Homepage = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.selectedCountry_Homepage;
    }
  }
);
export const selectSelectedCountryProvince_Homepage = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return {
        country: state.selectedCountry_Homepage,
        province: state.selectedProvince_Homepage,
      };
    }
  }
);
export const selectSelectedProvince_Homepage = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.selectedProvince_Homepage;
    }
  }
);
export const selectLoadingCountriesList_Admin = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.loadingCountriesList_Admin;
    }
  }
);
export const selectCountriesList_Admin = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.countriesList_Admin;
    }
  }
);
export const selectLoadingSaveCountry = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.loadingSaveCountry;
    }
  }
);
export const selectLoadingSaveProvince = createSelector(
  selectLocationState,
  (state: LocationState) => {
    {
      return state.loadingSaveProvince;
    }
  }
);
