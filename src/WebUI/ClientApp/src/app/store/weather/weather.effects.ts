import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { NzNotificationService } from "ng-zorro-antd/notification";
import {
  CountryDto,
  DailyForecastWeatherDto,
  HourlyForecastWeatherDto,
  UpdateOrCreateWeatherDataCommand,
  WeatherClient,
} from "src/app/web-api-client";
import { exhaustMap, catchError, of, map, tap, withLatestFrom } from "rxjs";
import {
  loadHistoricalStats_failure,
  loadHistoricalStats_success,
  loadWeather,
  loadWeatherConditionsFailure,
  loadWeatherConditionsSuccess,
  loadWeathersFailure,
  loadWeathersSuccess,
  saveWeatherData_failure,
  saveWeatherData_success,
  WeatherActions,
} from "./weather.actions";
import { Guid } from "guid-typescript";
import {
  LocationActions,
  selectProvince_Homepage,
} from "../location/location.actions";
import { Store } from "@ngrx/store";
import {
  selectSelectedCountryProvince_Homepage,
  selectSelectedCountry_Homepage,
  selectSelectedProvince_Homepage,
} from "../location/location.selectors";
import * as moment from "moment-timezone";
import { selectWeatherConditions } from "./weather.selectors";
export enum DayTime {
  Morning,
  Afternoon,
  Evening,
  Night,
}
export class WeatherInDay {
  weatherId: number;
  weatherDesc: string;
  weatherIcon: string;
  weatherMain: string;
  weatherIconUrl: string;
}
@Injectable()
export class WeatherEffects {
  selectProvince_Homepage$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SELECTPROVINCE_HOMEPAGE),
      exhaustMap(({ selectedProvince }) => {
        if (selectedProvince) {
          return of(loadWeather({ provinceId: selectedProvince["id"] }));
        } else {
          return of(
            loadWeathersFailure({
              error: {
                title: "Load Weather Failed",
                message: "Province is empty",
              },
            })
          );
        }
      })
    )
  );
  loadWeather$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WeatherActions.LOADWEATHER),
      withLatestFrom(this.store.select(selectSelectedCountry_Homepage)),
      exhaustMap(([{ provinceId }, country]) => {
        let requestId = Guid.create().toString();
        return this.weatherClient
          .getForecastWeatherIn7Days(provinceId, requestId)
          .pipe(
            map((res) => {
              //refractor data
              if (res.data.daily && res.data.hourly) {
                res.data.daily.forEach((element) => {
                  let morningForecast = this.getDefaultWeatherForecast(
                    element.dt,
                    DayTime.Morning,
                    country,
                    res.data.hourly
                  );
                  if (morningForecast && element.weatherId_morn === 0) {
                    element.weatherId_morn = morningForecast.weatherId;
                    element.weatherDesc_morn = morningForecast.weatherDesc;
                    element.weatherMain_morn = morningForecast.weatherMain;
                    element.weatherIcon_morn = morningForecast.weatherIcon;
                    element.weatherIcon_morn_url =
                      morningForecast.weatherIconUrl;
                  }

                  let afternoonForecast = this.getDefaultWeatherForecast(
                    element.dt,
                    DayTime.Afternoon,
                    country,
                    res.data.hourly
                  );
                  if (afternoonForecast && element.weatherId_day === 0) {
                    element.weatherId_day = afternoonForecast.weatherId;
                    element.weatherDesc_day = afternoonForecast.weatherDesc;
                    element.weatherMain_day = afternoonForecast.weatherMain;
                    element.weatherIcon_day = afternoonForecast.weatherIcon;
                    element.weatherIcon_day_url =
                      afternoonForecast.weatherIconUrl;
                  }
                  let eveningForecast = this.getDefaultWeatherForecast(
                    element.dt,
                    DayTime.Evening,
                    country,
                    res.data.hourly
                  );
                  if (eveningForecast && element.weatherId_eve === 0) {
                    element.weatherId_eve = eveningForecast.weatherId;
                    element.weatherDesc_eve = eveningForecast.weatherDesc;
                    element.weatherMain_eve = eveningForecast.weatherMain;
                    element.weatherIcon_eve = eveningForecast.weatherIcon;
                    element.weatherIcon_eve_url =
                      eveningForecast.weatherIconUrl;
                  }
                  let nightForecast = this.getDefaultWeatherForecast(
                    element.dt,
                    DayTime.Night,
                    country,
                    res.data.hourly
                  );
                  if (nightForecast && element.weatherId_night === 0) {
                    element.weatherId_night = nightForecast.weatherId;
                    element.weatherDesc_night = nightForecast.weatherDesc;
                    element.weatherMain_night = nightForecast.weatherMain;
                    element.weatherIcon_night = nightForecast.weatherIcon;
                    element.weatherIcon_night_url =
                      nightForecast.weatherIconUrl;
                  }
                });
              }
              return loadWeathersSuccess({ weatherData: res.data });
            }),
            catchError((error) => {
              console.log(error.response);
              return of(
                loadWeathersFailure({
                  error: JSON.parse(error.response).message,
                })
              );
            })
          );
      })
    )
  );
  loadWeathersFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(WeatherActions.LOADWEATHER_FAILURE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  loadHistoricalStats$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WeatherActions.LOADHISTORICALSTATS),
      withLatestFrom(this.store.select(selectSelectedCountryProvince_Homepage)),
      exhaustMap(([{ currentWeatherData }, { country, province }]) => {
        if (currentWeatherData && province && country) {
          return this.weatherClient
            .getLastLocalHistoricalWeatherQuery(
              province.id,
              currentWeatherData["dt"],
              //1687885200,
              4,
              1,
              99,
              Guid.create().toString()
            )
            .pipe(
              map((res) => {
                if (res.data.items.length === 0) {
                  return loadHistoricalStats_success({
                    data: null,
                  });
                }
                let historicalWeatheData = res.data.items;
                let dataset = [];
                historicalWeatheData.forEach((element) => {
                  dataset.push({
                    label: this.getDateTime(element.dt, true, country).format(
                      "MMM D,yyyy"
                    ),
                    data: [
                      Math.ceil(element.temp_morn),
                      Math.ceil(element.temp_day),
                      Math.ceil(element.temp_eve),
                      Math.ceil(element.temp_night),
                    ],
                  });
                });
                dataset.push({
                  label: this.getDateTime(
                    currentWeatherData["dt"],
                    true,
                    country
                  ).format("MMM D,yyyy"),
                  data: [
                    Math.ceil(currentWeatherData["temp_morn"]),
                    Math.ceil(currentWeatherData["temp_day"]),
                    Math.ceil(currentWeatherData["temp_eve"]),
                    Math.ceil(currentWeatherData["temp_night"]),
                  ],
                });
                let chartData = {
                  labels: ["Morning", "Afternoon", "Evening", "Night"],
                  datasets: dataset,
                };
                return loadHistoricalStats_success({
                  data: {
                    chartData: chartData,
                    historicalData: historicalWeatheData,
                  },
                });
              })
            );
        } else {
          return of(
            loadHistoricalStats_failure({
              error: {
                title: "Load Historical Stats Failed",
                message: "missing data for selected weather or province",
              },
            })
          );
        }
      })
    )
  );

  loadHistoricalStatsFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(WeatherActions.LOADHISTORICALSTATS_FAILURE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );

  LoadWeatherConditions$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WeatherActions.LOADWEATHERCONDITIONS),
      withLatestFrom(this.store.select(selectWeatherConditions)),
      exhaustMap(([{}, weatherCondition]) => {
        if (weatherCondition) {
          return of(
            loadWeatherConditionsSuccess({
              weatherConditionsData: weatherCondition,
            })
          );
        }
        return this.weatherClient
          .getWeatherConditions(Guid.create().toString())
          .pipe(
            map((res) =>
              loadWeatherConditionsSuccess({ weatherConditionsData: res.data })
            ),
            catchError((error) => {
              console.log(error.response);
              return of(
                loadWeatherConditionsFailure({
                  error: JSON.parse(error.response).message,
                })
              );
            })
          );
      })
    )
  );
  saveWeatherData$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WeatherActions.SAVEWEATHERDATA),
      exhaustMap(({ data }) => {
        let _data: DailyForecastWeatherDto =
          DailyForecastWeatherDto.fromJS(data);
        return this.weatherClient
          .updateOrCreateWeatherData(
            UpdateOrCreateWeatherDataCommand.fromJS(data)
          )
          .pipe(
            map((res) => {
              this.message.success("Save Changes", "Successfully");
              _data.id = res.data;
              return saveWeatherData_success({ data: _data });
            }),
            catchError((error) => {
              console.log(error.response);
              return of(
                saveWeatherData_failure({
                  error: JSON.parse(error.response).message,
                })
              );
            })
          );
      })
    )
  );
  saveWeatherData_success$ = createEffect(() =>
    this.actions$.pipe(
      ofType(WeatherActions.SAVEWEATHERDATA_SUCCESS),
      withLatestFrom(this.store.select(selectSelectedProvince_Homepage)),
      exhaustMap(([{}, province]) => {
        return of(loadWeather({ provinceId: province.id }));
      })
    )
  );
  loadWeatherConditionsFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(WeatherActions.LOADWEATHERCONDITIONS_FAILURE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  saveWeatherData_failure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(WeatherActions.SAVEWEATHERDATA_FAILURE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  private getDateTime(
    inputDt: number,
    isUnix: boolean = true,
    country: CountryDto
  ): moment.Moment {
    if (isUnix)
      return moment.unix(inputDt).tz(country.userDefined3 ?? moment.tz.guess());
    else return moment(inputDt).tz(country.userDefined3 ?? moment.tz.guess());
  }
  private getDefaultWeatherForecast(
    currentDt: number,
    dayTime: DayTime,
    country: CountryDto,
    weatherHourlyForecastData: HourlyForecastWeatherDto[]
  ): WeatherInDay | null {
    let currentMoment = this.getDateTime(currentDt, true, country);
    if (weatherHourlyForecastData) {
      switch (dayTime) {
        case DayTime.Morning:
          let topMorningTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              12,
              0,
              0,
              0
            ).getTime() / 1000;
          let bottomMorningTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              6,
              0,
              0,
              0
            ).getTime() / 1000;
          let listMornDt = weatherHourlyForecastData.filter(
            (x) => x.dt <= topMorningTime && x.dt >= bottomMorningTime
          );

          if (listMornDt.length > 0) {
            var morningForecast =
              listMornDt[Math.ceil((listMornDt.length - 1) / 2)];
            return {
              weatherId: morningForecast.weather_id,
              weatherDesc: morningForecast.weather_description,
              weatherIcon: morningForecast.weather_icon,
              weatherMain: morningForecast.weather_main,
              weatherIconUrl: morningForecast.weather_icon_url,
            };
          }
          break;
        case DayTime.Afternoon:
          let topAfternoonTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              17,
              0,
              0,
              0
            ).getTime() / 1000;
          let bottomAfternoonTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              12,
              0,
              0,
              0
            ).getTime() / 1000;
          let listAfternoonDt = weatherHourlyForecastData.filter(
            (x) => x.dt <= topAfternoonTime && x.dt >= bottomAfternoonTime
          );
          if (listAfternoonDt.length > 0) {
            var afternoonForecast =
              listAfternoonDt[Math.ceil((listAfternoonDt.length - 1) / 2)];
            return {
              weatherId: afternoonForecast.weather_id,
              weatherDesc: afternoonForecast.weather_description,
              weatherIcon: afternoonForecast.weather_icon,
              weatherMain: afternoonForecast.weather_main,
              weatherIconUrl: afternoonForecast.weather_icon_url,
            };
          }
          break;
        case DayTime.Evening:
          let topEveTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              21,
              0,
              0,
              0
            ).getTime() / 1000;
          let bottomEveTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              17,
              0,
              0,
              0
            ).getTime() / 1000;
          let listEveDt = weatherHourlyForecastData.filter(
            (x) => x.dt <= topEveTime && x.dt >= bottomEveTime
          );
          if (listEveDt.length > 0) {
            var eveForecast = listEveDt[Math.ceil((listEveDt.length - 1) / 2)];
            return {
              weatherId: eveForecast.weather_id,
              weatherDesc: eveForecast.weather_description,
              weatherIcon: eveForecast.weather_icon,
              weatherMain: eveForecast.weather_main,
              weatherIconUrl: eveForecast.weather_icon_url,
            };
          }
          break;
        case DayTime.Night:
          let topNightTime =
            (new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              6,
              0,
              0,
              0
            ).getTime() +
              86400000) /
            1000;
          let bottomNightTime =
            new Date(
              currentMoment.year(),
              currentMoment.month(),
              currentMoment.date(),
              21,
              0,
              0,
              0
            ).getTime() / 1000;
          let listNightDt = weatherHourlyForecastData.filter(
            (x) => x.dt <= topNightTime && x.dt >= bottomNightTime
          );
          if (listNightDt.length > 0) {
            var nightForecast =
              listNightDt[Math.ceil((listNightDt.length - 1) / 2)];
            return {
              weatherId: nightForecast.weather_id,
              weatherDesc: nightForecast.weather_description,
              weatherIcon: nightForecast.weather_icon,
              weatherMain: nightForecast.weather_main,
              weatherIconUrl: nightForecast.weather_icon_url,
            };
          }
          break;
      }
    }
    return null;
  }
  constructor(
    private actions$: Actions,
    private weatherClient: WeatherClient,
    private message: NzNotificationService,
    private store: Store
  ) {}
}
