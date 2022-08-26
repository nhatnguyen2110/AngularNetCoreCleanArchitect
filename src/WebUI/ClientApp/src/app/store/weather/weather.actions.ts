import { createAction, props } from "@ngrx/store";
import { HistoricalWeather } from "src/app/models/historicalweather.model";
import {
  CountryDto,
  DailyForecastWeatherDto,
  WeatherConditionCollectionDto,
  WeatherForecastDto,
} from "src/app/web-api-client";
export enum WeatherActions {
  LOADWEATHER = "[Weather] Load Weather",
  LOADWEATHER_SUCCESS = "[Weather] Load Weather success",
  LOADWEATHER_FAILURE = "[Weather] Load Weather failure",
  LOADHISTORICALSTATS = "[Weather] Load Historical Stats",
  LOADHISTORICALSTATS_SUCCESS = "[Weather] Load Historical Stats success",
  LOADHISTORICALSTATS_FAILURE = "[Weather] Load Historical Stats failure",
  SETCURRENTWEATHER = "[Weather] Set Current Weather Data",
  SAVEWEATHERDATA = "[Weather] Save Weather Data",
  SAVEWEATHERDATA_SUCCESS = "[Weather] Save Weather Data Success",
  SAVEWEATHERDATA_FAILURE = "[Weather] Save Weather Data Failure",
  UPDATEWEATHERMODALSTATUS = "[Weather] Update Weather Modal Status",
  LOADWEATHERCONDITIONS = "[Weather] Load Weather Conditions",
  LOADWEATHERCONDITIONS_SUCCESS = "[Weather] Load Weather Conditions success",
  LOADWEATHERCONDITIONS_FAILURE = "[Weather] Load Weather Conditions failure",
}
export const loadWeather = createAction(
  WeatherActions.LOADWEATHER,
  props<{ provinceId: number }>()
);

export const loadWeathersSuccess = createAction(
  WeatherActions.LOADWEATHER_SUCCESS,
  props<{ weatherData: WeatherForecastDto }>()
);

export const loadWeathersFailure = createAction(
  WeatherActions.LOADWEATHER_FAILURE,
  props<{ error: any }>()
);
export const loadHistoricalStats = createAction(
  WeatherActions.LOADHISTORICALSTATS,
  props<{
    currentWeatherData: DailyForecastWeatherDto;
  }>()
);
export const loadHistoricalStats_success = createAction(
  WeatherActions.LOADHISTORICALSTATS_SUCCESS,
  props<{
    data: HistoricalWeather;
  }>()
);
export const loadHistoricalStats_failure = createAction(
  WeatherActions.LOADHISTORICALSTATS_FAILURE,
  props<{ error: any }>()
);
export const setCurrentWeather = createAction(
  WeatherActions.SETCURRENTWEATHER,
  props<{ data: DailyForecastWeatherDto }>()
);
export const saveWeatherData = createAction(
  WeatherActions.SAVEWEATHERDATA,
  props<{
    data: DailyForecastWeatherDto;
  }>()
);
export const saveWeatherData_success = createAction(
  WeatherActions.SAVEWEATHERDATA_SUCCESS,
  props<{
    data: DailyForecastWeatherDto;
  }>()
);
export const saveWeatherData_failure = createAction(
  WeatherActions.SAVEWEATHERDATA_FAILURE,
  props<{ error: any }>()
);
export const updateWeatherModalStatus = createAction(
  WeatherActions.UPDATEWEATHERMODALSTATUS,
  props<{ status: boolean }>()
);
export const loadWeatherConditions = createAction(
  WeatherActions.LOADWEATHERCONDITIONS
);

export const loadWeatherConditionsSuccess = createAction(
  WeatherActions.LOADWEATHERCONDITIONS_SUCCESS,
  props<{ weatherConditionsData: WeatherConditionCollectionDto }>()
);

export const loadWeatherConditionsFailure = createAction(
  WeatherActions.LOADWEATHERCONDITIONS_FAILURE,
  props<{ error: any }>()
);
