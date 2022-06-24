import { Component, OnInit, TemplateRef } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import {
  CountriesClient,
  CountryDto,
  CreateWeatherDataCommand,
  DailyForecastWeatherDto,
  GetWeatherConditionQuery,
  HourlyForecastWeatherDto,
  OWPWeatherCondition,
  ProvinceDto,
  ProvincesClient,
  UpdateWeatherDataCommand,
  WeatherClient,
  WeatherConditionCollectionDto,
} from "../web-api-client";
import { NotifierService } from "angular-notifier";
import * as moment from "moment-timezone";
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
import { Observable } from "rxjs";
import { AuthorizeService } from "src/api-authorization/authorize.service";
import { TestBed } from "@angular/core/testing";
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
  faAngleDoubleDown = faAngleDoubleDown;
  faTint = faTint;
  faCloudRain = faCloudRain;
  faCloudSun = faCloudSun;
  faCloud = faCloud;
  faCloudShowersHeavy = faCloudShowersHeavy;
  faWind = faWind;
  faSmog = faSmog;
  weatherForecastData: DailyForecastWeatherDto[];
  weatherHourlyForecastData: HourlyForecastWeatherDto[];
  selectedDt: DailyForecastWeatherDto;
  isAuthenticated: Observable<boolean>;
  faFile = faFile;
  faFileAlt = faFileAlt;
  editWeatherForm: FormGroup;
  editWeatherModalRef: BsModalRef;
  weatherConditions: WeatherConditionCollectionDto;
  debug=true;
  constructor(
    private countriesClient: CountriesClient,
    private provincesClient: ProvincesClient,
    private weatherClient: WeatherClient,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private notifier: NotifierService,
    private authorizeService: AuthorizeService
  ) {
    countriesClient.getList(null, 1, 99).subscribe(
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
            var selectedProvinceId = localStorage.getItem("selectedProvinceId");
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

  ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.editWeatherForm = this.fb.group({
      id: 0,
      dt: 0,
      provinceId: 0,
      sunrise: 0,
      sunset: 0,
      temp_avg: [0, Validators.compose([Validators.required])],
      temp_min: [0, Validators.compose([Validators.required])],
      temp_max: [0, Validators.compose([Validators.required])],
      humidity: [0, Validators.compose([Validators.required])],
      dew_point: [0, Validators.compose([Validators.required])],
      pop: [0, Validators.compose([Validators.required])],
      wind_speed: [0, Validators.compose([Validators.required])],
      clouds: [0, Validators.compose([Validators.required])],
      weather_id: [
        0,
        Validators.compose([Validators.required, Validators.min(1)]),
      ],
      weather_main: "",
      weather_description: "",
      weather_icon: "",
      weather_icon_url: "",
      temp_morn: [0, Validators.compose([Validators.required])],
      temp_day: [0, Validators.compose([Validators.required])],
      temp_eve: [0, Validators.compose([Validators.required])],
      temp_night: [0, Validators.compose([Validators.required])],
      weatherId_morn: [
        0,
        Validators.compose([Validators.required, Validators.min(1)]),
      ],
      weatherMain_morn: "",
      weatherDesc_morn: "",
      weatherIcon_morn: "",
      weatherIcon_morn_url: "",
      weatherId_day: [
        0,
        Validators.compose([Validators.required, Validators.min(1)]),
      ],
      weatherMain_day: "",
      weatherDesc_day: "",
      weatherIcon_day: "",
      weatherIcon_day_url: "",
      weatherId_eve: [
        0,
        Validators.compose([Validators.required, Validators.min(1)]),
      ],
      weatherMain_eve: "",
      weatherDesc_eve: "",
      weatherIcon_eve: "",
      weatherIcon_eve_url: "",
      weatherId_night: [
        0,
        Validators.compose([Validators.required, Validators.min(1)]),
      ],
      weatherMain_night: "",
      weatherDesc_night: "",
      weatherIcon_night: "",
      weatherIcon_night_url: "",
    });
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
  getCurrentDateTime() {
    this.currentDateTime = this.getDateTime(new Date().getTime(), false);
  }
  getForecastWeatherIn7Days() {
    this.collapseForecastDetail();
    if (this.selectedProvince) {
      this.weatherClient
        .getForecastWeatherIn7Days(this.selectedProvince.id)
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
          2;
      }, 100);
    }
  }
  collapseForecastDetail() {
    this.selectedDt = null;
  }
  showWeatherDetailModal(template: TemplateRef<any>): void {
    if (this.weatherConditions) {
      this.loadWeatherDetailModal(template);
    } else {
      this.weatherClient
        .getWeatherConditions(new GetWeatherConditionQuery())
        .subscribe(
          (result) => {
            this.weatherConditions = result.data;
            this.loadWeatherDetailModal(template);
          },
          (error) => console.error(error)
        );
    }
  }
  loadWeatherDetailModal(template: TemplateRef<any>) {
    let morningForecast = this.getDefaultWeatherForecast(
      this.selectedDt.dt,
      DayTime.Morning
    );
    let afternoonForecast = this.getDefaultWeatherForecast(
      this.selectedDt.dt,
      DayTime.Afternoon
    );
    let eveningForecast = this.getDefaultWeatherForecast(
      this.selectedDt.dt,
      DayTime.Evening
    );
    let nightForecast = this.getDefaultWeatherForecast(
      this.selectedDt.dt,
      DayTime.Night
    );
    this.editWeatherForm.setValue({
      id: this.selectedDt.id,
      dt: this.selectedDt.dt,
      provinceId: this.selectedProvince.id,
      sunrise: this.selectedDt.sunrise,
      sunset: this.selectedDt.sunset,
      temp_avg: this.selectedDt.temp_avg,
      temp_min: this.selectedDt.temp_min,
      temp_max: this.selectedDt.temp_max,
      humidity: this.selectedDt.humidity,
      dew_point: this.selectedDt.dew_point,
      pop: this.selectedDt.pop,
      wind_speed: this.selectedDt.wind_speed,
      clouds: this.selectedDt.clouds,
      weather_id: this.selectedDt.weather_id,
      weather_main: this.selectedDt.weather_main,
      weather_description: this.selectedDt.weather_description,
      weather_icon: this.selectedDt.weather_icon,
      weather_icon_url: this.selectedDt.weather_icon_url,
      temp_morn: this.selectedDt.temp_morn,
      temp_day: this.selectedDt.temp_day,
      temp_eve: this.selectedDt.temp_eve,
      temp_night: this.selectedDt.temp_night,
      weatherId_morn: this.selectedDt.weatherId_morn,
      weatherMain_morn: this.selectedDt.weatherMain_morn,
      weatherDesc_morn: this.selectedDt.weatherDesc_morn,
      weatherIcon_morn: this.selectedDt.weatherIcon_morn,
      weatherIcon_morn_url: this.selectedDt.weatherIcon_morn_url,
      weatherId_day: this.selectedDt.weatherId_day,
      weatherMain_day: this.selectedDt.weatherMain_day,
      weatherDesc_day: this.selectedDt.weatherDesc_day,
      weatherIcon_day: this.selectedDt.weatherIcon_day,
      weatherIcon_day_url: this.selectedDt.weatherIcon_day_url,
      weatherId_eve: this.selectedDt.weatherId_eve,
      weatherMain_eve: this.selectedDt.weatherMain_eve,
      weatherDesc_eve: this.selectedDt.weatherDesc_eve,
      weatherIcon_eve: this.selectedDt.weatherIcon_eve,
      weatherIcon_eve_url: this.selectedDt.weatherIcon_eve_url,
      weatherId_night: this.selectedDt.weatherId_night,
      weatherMain_night: this.selectedDt.weatherMain_night,
      weatherDesc_night: this.selectedDt.weatherDesc_night,
      weatherIcon_night: this.selectedDt.weatherIcon_night,
      weatherIcon_night_url: this.selectedDt.weatherIcon_night_url,
    });
    this.editWeatherModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("temp_avg").focus(), 250);
  }
  updateWeatherDetails() {
    if (this.selectedDt.id > 0) {
      this.weatherClient
        .update(UpdateWeatherDataCommand.fromJS(this.editWeatherForm.value))
        .subscribe(
          (result) => {
            this.selectedDt.sunset = this.editWeatherForm.value.sunset;
            this.selectedDt.temp_avg = this.editWeatherForm.value.temp_avg;
            this.selectedDt.temp_min = this.editWeatherForm.value.temp_min;
            this.selectedDt.temp_max = this.editWeatherForm.value.temp_max;
            this.selectedDt.humidity = this.editWeatherForm.value.humidity;
            this.selectedDt.dew_point = this.editWeatherForm.value.dew_point;
            this.selectedDt.pop = this.editWeatherForm.value.pop;
            this.selectedDt.wind_speed = this.editWeatherForm.value.wind_speed;
            this.selectedDt.clouds = this.editWeatherForm.value.clouds;
            this.selectedDt.weather_id = this.editWeatherForm.value.weather_id;
            this.selectedDt.weather_main =
              this.editWeatherForm.value.weather_main;
            this.selectedDt.weather_description =
              this.editWeatherForm.value.weather_description;
            this.selectedDt.weather_icon =
              this.editWeatherForm.value.weather_icon;
              this.selectedDt.weather_icon_url = this.editWeatherForm.value.weather_icon_url;
            this.selectedDt.temp_morn = this.editWeatherForm.value.temp_morn;
            this.selectedDt.temp_day = this.editWeatherForm.value.temp_day;
            this.selectedDt.temp_eve = this.editWeatherForm.value.temp_eve;
            this.selectedDt.temp_night = this.editWeatherForm.value.temp_night;
            this.selectedDt.weatherId_morn =
              this.editWeatherForm.value.weatherId_morn;
            this.selectedDt.weatherMain_morn =
              this.editWeatherForm.value.weatherMain_morn;
            this.selectedDt.weatherDesc_morn =
              this.editWeatherForm.value.weatherDesc_morn;
            this.selectedDt.weatherIcon_morn =
              this.editWeatherForm.value.weatherIcon_morn;
              this.selectedDt.weatherIcon_morn_url =
              this.editWeatherForm.value.weatherIcon_morn_url;
            this.selectedDt.weatherId_day =
              this.editWeatherForm.value.weatherId_day;
            this.selectedDt.weatherMain_day =
              this.editWeatherForm.value.weatherMain_day;
            this.selectedDt.weatherDesc_day =
              this.editWeatherForm.value.weatherDesc_day;
            this.selectedDt.weatherIcon_day =
              this.editWeatherForm.value.weatherIcon_day;
              this.selectedDt.weatherIcon_day_url =
              this.editWeatherForm.value.weatherIcon_day_url;
            this.selectedDt.weatherId_eve =
              this.editWeatherForm.value.weatherId_eve;
            this.selectedDt.weatherMain_eve =
              this.editWeatherForm.value.weatherMain_eve;
            this.selectedDt.weatherDesc_eve =
              this.editWeatherForm.value.weatherDesc_eve;
            this.selectedDt.weatherIcon_eve =
              this.editWeatherForm.value.weatherIcon_eve;
              this.selectedDt.weatherIcon_eve_url =
              this.editWeatherForm.value.weatherIcon_eve_url;
            this.selectedDt.weatherId_night =
              this.editWeatherForm.value.weatherId_night;
            this.selectedDt.weatherMain_night =
              this.editWeatherForm.value.weatherMain_night;
            this.selectedDt.weatherDesc_night =
              this.editWeatherForm.value.weatherDesc_night;
            this.selectedDt.weatherIcon_night =
              this.editWeatherForm.value.weatherIcon_night;
              this.selectedDt.weatherIcon_night_url =
              this.editWeatherForm.value.weatherIcon_night_url;
            this.editWeatherModalRef.hide();
            this.notifier.notify("success", "Save changes successfully");
          },
          (error) => {
            console.error(error);
            let errors = JSON.parse(error.response);
            this.notifier.notify("error", errors.message);
          }
        );
    } else {
      this.weatherClient
        .create(CreateWeatherDataCommand.fromJS(this.editWeatherForm.value))
        .subscribe(
          (result) => {
            this.selectedDt.id = result.data;
            this.selectedDt.sunset = this.editWeatherForm.value.sunset;
            this.selectedDt.temp_avg = this.editWeatherForm.value.temp_avg;
            this.selectedDt.temp_min = this.editWeatherForm.value.temp_min;
            this.selectedDt.temp_max = this.editWeatherForm.value.temp_max;
            this.selectedDt.humidity = this.editWeatherForm.value.humidity;
            this.selectedDt.dew_point = this.editWeatherForm.value.dew_point;
            this.selectedDt.pop = this.editWeatherForm.value.pop;
            this.selectedDt.wind_speed = this.editWeatherForm.value.wind_speed;
            this.selectedDt.clouds = this.editWeatherForm.value.clouds;
            this.selectedDt.weather_id = this.editWeatherForm.value.weather_id;
            this.selectedDt.weather_main =
              this.editWeatherForm.value.weather_main;
            this.selectedDt.weather_description =
              this.editWeatherForm.value.weather_description;
            this.selectedDt.weather_icon =
              this.editWeatherForm.value.weather_icon;
              this.selectedDt.weather_icon_url =
              this.editWeatherForm.value.weather_icon_url;
            this.selectedDt.temp_morn = this.editWeatherForm.value.temp_morn;
            this.selectedDt.temp_day = this.editWeatherForm.value.temp_day;
            this.selectedDt.temp_eve = this.editWeatherForm.value.temp_eve;
            this.selectedDt.temp_night = this.editWeatherForm.value.temp_night;
            this.selectedDt.weatherId_morn =
              this.editWeatherForm.value.weatherId_morn;
            this.selectedDt.weatherMain_morn =
              this.editWeatherForm.value.weatherMain_morn;
            this.selectedDt.weatherDesc_morn =
              this.editWeatherForm.value.weatherDesc_morn;
            this.selectedDt.weatherIcon_morn =
              this.editWeatherForm.value.weatherIcon_morn;
              this.selectedDt.weatherIcon_morn_url =
              this.editWeatherForm.value.weatherIcon_morn_url;
            this.selectedDt.weatherId_day =
              this.editWeatherForm.value.weatherId_day;
            this.selectedDt.weatherMain_day =
              this.editWeatherForm.value.weatherMain_day;
            this.selectedDt.weatherDesc_day =
              this.editWeatherForm.value.weatherDesc_day;
            this.selectedDt.weatherIcon_day =
              this.editWeatherForm.value.weatherIcon_day;
              this.selectedDt.weatherIcon_day_url =
              this.editWeatherForm.value.weatherIcon_day_url;
            this.selectedDt.weatherId_eve =
              this.editWeatherForm.value.weatherId_eve;
            this.selectedDt.weatherMain_eve =
              this.editWeatherForm.value.weatherMain_eve;
            this.selectedDt.weatherDesc_eve =
              this.editWeatherForm.value.weatherDesc_eve;
            this.selectedDt.weatherIcon_eve =
              this.editWeatherForm.value.weatherIcon_eve;
              this.selectedDt.weatherIcon_eve_url =
              this.editWeatherForm.value.weatherIcon_eve_url;
            this.selectedDt.weatherId_night =
              this.editWeatherForm.value.weatherId_night;
            this.selectedDt.weatherMain_night =
              this.editWeatherForm.value.weatherMain_night;
            this.selectedDt.weatherDesc_night =
              this.editWeatherForm.value.weatherDesc_night;
            this.selectedDt.weatherIcon_night =
              this.editWeatherForm.value.weatherIcon_night;
              this.selectedDt.weatherIcon_night_url =
              this.editWeatherForm.value.weatherIcon_night_url;
            this.editWeatherModalRef.hide();
            this.notifier.notify("success", "Save changes successfully");
          },
          (error) => {
            console.error(error);
            let errors = JSON.parse(error.response);
            this.notifier.notify("error", errors.message);
          }
        );
    }
  }
  customSearchFn(term: string, item: OWPWeatherCondition) {
    term = term.toLowerCase();
    return (
      item.description.toLowerCase().indexOf(term) > -1 ||
      item.main.toLowerCase() === term
    );
  }
  onWeatherChange($event) {
    this.editWeatherForm.patchValue({
      weather_main: $event.main,
      weather_description: $event.description,
      weather_icon: $event.icon,
      weather_icon_url: $event.iconUrl
    });
  }
  onAfternoonWeatherChange($event) {
    this.editWeatherForm.patchValue({
      weatherMain_day: $event.main,
      weatherDesc_day: $event.description,
      weatherIcon_day: $event.icon,
      weatherIcon_day_url: $event.iconUrl,
    });
  }
  onMorningWeatherChange($event) {
    this.editWeatherForm.patchValue({
      weatherMain_morn: $event.main,
      weatherDesc_morn: $event.description,
      weatherIcon_morn: $event.icon,
      weatherIcon_morn_url: $event.iconUrl,
    });
  }
  onEveningWeatherChange($event) {
    this.editWeatherForm.patchValue({
      weatherMain_eve: $event.main,
      weatherDesc_eve: $event.description,
      weatherIcon_eve: $event.icon,
      weatherIcon_eve_url: $event.iconUrl,
    });
  }
  onNightWeatherChange($event) {
    this.editWeatherForm.patchValue({
      weatherMain_night: $event.main,
      weatherDesc_night: $event.description,
      weatherIcon_night: $event.icon,
      weatherIcon_night_url: $event.iconUrl,
    });
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
}
