import { Action, createReducer, on } from "@ngrx/store";
import { HistoricalWeather } from "src/app/models/historicalweather.model";
import {
  DailyForecastWeatherDto,
  WeatherConditionCollectionDto,
  WeatherForecastDto,
} from "src/app/web-api-client";
import {
  loadHistoricalStats,
  loadHistoricalStats_failure,
  loadHistoricalStats_success,
  loadWeather,
  loadWeatherConditionsSuccess,
  loadWeathersFailure,
  loadWeathersSuccess,
  saveWeatherData,
  saveWeatherData_failure,
  saveWeatherData_success,
  setCurrentWeather,
  updateWeatherModalStatus,
} from "./weather.actions";

export const weatherFeatureKey = "weather";

export interface WeatherState {
  weatherData: WeatherForecastDto | null;
  loadingWeatherData: boolean;
  historicalWeather: HistoricalWeather | null;
  loadingHistoricalData: boolean;
  loadingSaveWeatherData: boolean;
  currentWeatherData: DailyForecastWeatherDto | null;
  displayWeatherModalStatus: boolean;
  weatherConditionData: WeatherConditionCollectionDto | null;
}

export const initialWeatherState: WeatherState = {
  weatherData: null,
  loadingWeatherData: false,
  historicalWeather: null,
  loadingHistoricalData: false,
  loadingSaveWeatherData: false,
  currentWeatherData: null,
  displayWeatherModalStatus: false,
  weatherConditionData: null
};

export const weatherReducer = createReducer(
  initialWeatherState,
  on(loadWeather, (state) => {
    return {
      ...state,
      loadingWeatherData: true,
      weatherData: null,
    };
  }),
  on(loadWeathersSuccess, (state, { weatherData }) => {
    return {
      ...state,
      loadingWeatherData: false,
      weatherData: weatherData,
    };
  }),
  on(loadWeathersFailure, (state) => {
    return {
      ...state,
      loadingWeatherData: false,
      weatherData: null,
    };
  }),
  on(loadHistoricalStats, (state) => {
    return {
      ...state,
      loadingHistoricalData: true,
      historicalWeather: null,
    };
  }),
  on(loadHistoricalStats_failure, (state) => {
    return {
      ...state,
      loadingHistoricalData: false,
      historicalWeather: null,
    };
  }),
  on(loadHistoricalStats_success, (state, { data }) => {
    return {
      ...state,
      loadingHistoricalData: false,
      historicalWeather: data,
    };
  }),
  on(setCurrentWeather, (state, { data }) => {
    if (data && data === state.currentWeatherData) {
      // hide current data if user click double times
      return {
        ...state,
        currentWeatherData: null,
      };
    }
    return {
      ...state,
      currentWeatherData: data,
    };
  }),
  on(saveWeatherData, (state) => {
    return {
      ...state,
      loadingSaveWeatherData: true,
    };
  }),
  on(saveWeatherData_success, (state, { data }) => {
    return {
      ...state,
      loadingSaveWeatherData: false,
      currentWeatherData: data,
    };
  }),
  on(saveWeatherData_failure, (state) => {
    return {
      ...state,
      loadingSaveWeatherData: false,
    };
  }),
  on(updateWeatherModalStatus, (state, { status }) => {
    return {
      ...state,
      displayWeatherModalStatus: status,
    };
  }),
  on(loadWeatherConditionsSuccess, (state, {weatherConditionsData}) => {
    return {
      ...state,
      weatherConditionData: weatherConditionsData,
    };
  }),
);
