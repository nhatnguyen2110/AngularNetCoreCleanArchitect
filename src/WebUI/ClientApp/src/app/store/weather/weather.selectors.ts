import { createFeatureSelector, createSelector } from "@ngrx/store";
import { weatherFeatureKey, WeatherState } from "./weather.reducer";

export const selectWeahtherState =
  createFeatureSelector<WeatherState>(weatherFeatureKey);
export const selectWeatherData = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.weatherData;
    }
  }
);
export const selectLoadingWeatherData = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.loadingWeatherData;
    }
  }
);
export const selectLoadingHistoricalWeatherData = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.loadingHistoricalData;
    }
  }
);
export const selectHistoricalStats = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.historicalWeather;
    }
  }
);
export const selectCurrentWeatherData = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.currentWeatherData;
    }
  }
);
export const selectWeatherModalStatus = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.displayWeatherModalStatus;
    }
  }
);
export const selectWeatherConditions = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.weatherConditionData;
    }
  }
);
export const selectLoadingSaveWeatherData = createSelector(
  selectWeahtherState,
  (state: WeatherState) => {
    {
      return state.loadingSaveWeatherData;
    }
  }
);
