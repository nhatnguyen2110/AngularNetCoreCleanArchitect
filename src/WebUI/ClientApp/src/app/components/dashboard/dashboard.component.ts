import { Component, OnInit } from "@angular/core";
import {
  CountriesClient,
  CountryDto,
  DailyForecastWeatherDto,
  HourlyForecastWeatherDto,
  ProvinceDto,
  WeatherClient,
} from "src/app/web-api-client";
import { Guid } from "guid-typescript";
import * as moment from "moment-timezone";
import { ChartData, ChartOptions } from "chart.js";
import {
  faAngleDoubleDown,
  faTint,
  faCloudRain,
  faCloudSun,
  faCloud,
  faCloudShowersHeavy,
  faWind,
  faSmog,
  faFile,
  faFileAlt,
} from "@fortawesome/free-solid-svg-icons";
import { ConfigService } from "src/app/services/config.service";
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
@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  countriesList?: CountryDto[];
  selectedCountry: CountryDto;
  selectedProvince: ProvinceDto;
  currentDateTime: moment.Moment;
  selectedTimezone: string;
  weatherForecastData: DailyForecastWeatherDto[];
  weatherHourlyForecastData: HourlyForecastWeatherDto[];
  selectedDt: DailyForecastWeatherDto;

  faAngleDoubleDown = faAngleDoubleDown;
  faTint = faTint;
  faCloudRain = faCloudRain;
  faCloudSun = faCloudSun;
  faCloud = faCloud;
  faCloudShowersHeavy = faCloudShowersHeavy;
  faWind = faWind;
  faSmog = faSmog;
  faFile = faFile;
  faFileAlt = faFileAlt;
  salesData: ChartData<"bar">;
  chartOptions: ChartOptions = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
      },
      title: {
        display: true,
        text: "Historical Temperature",
      },
    },
  };

  historicalWeatheData: DailyForecastWeatherDto[];
  constructor(
    private countriesClient: CountriesClient,
    private weatherClient: WeatherClient,
    private configService: ConfigService
  ) {}

  ngOnInit(): void {
    console.log(this.configService.systemConfig);
    this.countriesClient
      .getList(null, 1, 99, Guid.create().toString())
      .subscribe(
        (result) => {
          this.countriesList = result.data.items;
          if (this.countriesList.length) {
            var selectedCountryId = localStorage.getItem("selectedCountryId");
            if (selectedCountryId) {
              this.selectedCountry = this.countriesList.find(
                (x) => x.id == parseInt(selectedCountryId)
              );
            }
            if (!this.selectedCountry) {
              this.selectedCountry = this.countriesList[0];
            }
            if (this.selectedCountry && this.selectedCountry.provinces.length) {
              var selectedProvinceId =
                localStorage.getItem("selectedProvinceId");
              if (selectedProvinceId) {
                this.selectedProvince = this.selectedCountry.provinces.find(
                  (x) => x.id == parseInt(selectedProvinceId)
                );
              }
              if (!this.selectedProvince)
                this.selectedProvince = this.selectedCountry.provinces[0];
              this.getCurrentDateTime();
              this.getForecastWeatherIn7Days();
            }
          }
        },
        (error) => console.error(error)
      );
  }
  getCurrentDateTime() {
    this.currentDateTime = this.getDateTime(new Date().getTime(), false);
  }
  onChangeCountry(item: CountryDto) {
    localStorage.setItem("selectedCountryId", item.id.toString());
    localStorage.removeItem("selectedProvinceId");
    if (item.provinces) {
      this.selectedProvince = item.provinces[0];
    } else {
      this.selectedProvince = null;
    }
    this.getCurrentDateTime();
    this.getForecastWeatherIn7Days();
  }
  getForecastWeatherIn7Days() {
    this.collapseForecastDetail();
    if (this.selectedProvince) {
      this.weatherClient
        .getForecastWeatherIn7Days(
          this.selectedProvince.id,
          Guid.create().toString()
        )
        .subscribe((result) => {
          this.weatherForecastData = result.data.daily;
          this.weatherHourlyForecastData = result.data.hourly;
          if (this.weatherForecastData && this.weatherHourlyForecastData) {
            this.weatherForecastData.forEach((element) => {
              let morningForecast = this.getDefaultWeatherForecast(
                element.dt,
                DayTime.Morning
              );
              if (morningForecast && element.weatherId_morn === 0) {
                element.weatherId_morn = morningForecast.weatherId;
                element.weatherDesc_morn = morningForecast.weatherDesc;
                element.weatherMain_morn = morningForecast.weatherMain;
                element.weatherIcon_day = morningForecast.weatherIcon;
                element.weatherIcon_morn_url = morningForecast.weatherIconUrl;
              }

              let afternoonForecast = this.getDefaultWeatherForecast(
                element.dt,
                DayTime.Afternoon
              );
              if (afternoonForecast && element.weatherId_day === 0) {
                element.weatherId_day = afternoonForecast.weatherId;
                element.weatherDesc_day = afternoonForecast.weatherDesc;
                element.weatherMain_day = afternoonForecast.weatherMain;
                element.weatherIcon_day = afternoonForecast.weatherIcon;
                element.weatherIcon_day_url = afternoonForecast.weatherIconUrl;
              }
              let eveningForecast = this.getDefaultWeatherForecast(
                element.dt,
                DayTime.Evening
              );
              if (eveningForecast && element.weatherId_eve === 0) {
                element.weatherId_eve = eveningForecast.weatherId;
                element.weatherDesc_eve = eveningForecast.weatherDesc;
                element.weatherMain_eve = eveningForecast.weatherMain;
                element.weatherIcon_eve = eveningForecast.weatherIcon;
                element.weatherIcon_eve_url = eveningForecast.weatherIconUrl;
              }
              let nightForecast = this.getDefaultWeatherForecast(
                element.dt,
                DayTime.Night
              );
              if (nightForecast && element.weatherId_night === 0) {
                element.weatherId_night = nightForecast.weatherId;
                element.weatherDesc_night = nightForecast.weatherDesc;
                element.weatherMain_night = nightForecast.weatherMain;
                element.weatherIcon_night = nightForecast.weatherIcon;
                element.weatherIcon_night_url = nightForecast.weatherIconUrl;
              }
            });
          }
        });
      localStorage.setItem(
        "selectedProvinceId",
        this.selectedProvince.id.toString()
      );
    }
  }
  getDateTime(inputDt: number, isUnix: boolean = true): moment.Moment {
    if (this.selectedCountry.userDefined3) {
      this.selectedTimezone = this.selectedCountry.userDefined3;
    } else {
      this.selectedTimezone = moment.tz.guess();
    }
    if (isUnix) return moment.unix(inputDt).tz(this.selectedTimezone);
    else return moment(inputDt).tz(this.selectedTimezone);
  }
  loadForecastDetail(wfd: DailyForecastWeatherDto) {
    if (this.selectedDt == wfd) {
      this.collapseForecastDetail();
    } else {
      this.selectedDt = wfd;
      setTimeout(() => {
        document.getElementById("box-scrollmenu").scrollLeft =
          document.getElementById("dt-" + wfd.dt).getBoundingClientRect().left /
          3;
      }, 100);
      this.loadHistoricalStats();
    }
  }
  collapseForecastDetail() {
    this.selectedDt = null;
  }
  getDefaultWeatherForecast(
    currentDt: number,
    dayTime: DayTime
  ): WeatherInDay | null {
    let currentMoment = this.getDateTime(currentDt, true);
    if (this.weatherHourlyForecastData) {
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
          let listMornDt = this.weatherHourlyForecastData.filter(
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
          let listAfternoonDt = this.weatherHourlyForecastData.filter(
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
          let listEveDt = this.weatherHourlyForecastData.filter(
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
          let listNightDt = this.weatherHourlyForecastData.filter(
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
  loadHistoricalStats() {
    if (this.selectedDt) {
      this.weatherClient
        .getLastLocalHistoricalWeatherQuery(
          this.selectedProvince.id,
          this.selectedDt.dt,
          //1687885200,
          4,
          1,
          99,
          Guid.create().toString()
        )
        .subscribe(
          (result) => {
            this.historicalWeatheData = result.data.items;
            let dataset = [];
            this.historicalWeatheData.forEach((element) => {
              dataset.push({
                label: this.getDateTime(element.dt, true).format("MMM D,yyyy"),
                data: [
                  Math.ceil(element.temp_morn),
                  Math.ceil(element.temp_day),
                  Math.ceil(element.temp_eve),
                  Math.ceil(element.temp_night),
                ],
              });
            });
            dataset.push({
              label: this.getDateTime(this.selectedDt.dt, true).format(
                "MMM D,yyyy"
              ),
              data: [
                Math.ceil(this.selectedDt.temp_morn),
                Math.ceil(this.selectedDt.temp_day),
                Math.ceil(this.selectedDt.temp_eve),
                Math.ceil(this.selectedDt.temp_night),
              ],
            });
            this.salesData = {
              labels: ["Morning", "Afternoon", "Evening", "Night"],
              datasets: dataset,
            };
          },
          (error) => console.error(error)
        );
    }
  }
}
