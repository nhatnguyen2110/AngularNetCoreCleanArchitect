import {
  AfterViewChecked,
  ChangeDetectorRef,
  Component,
  OnInit,
} from "@angular/core";
import {
  CountryDto,
  DailyForecastWeatherDto,
  ProvinceDto,
} from "src/app/web-api-client";
import * as moment from "moment-timezone";
import { ChartOptions } from "chart.js";
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
import { Store } from "@ngrx/store";
import {
  selectCountriesList_Homepage,
  selectLoadingCountriesList_Homepage,
  selectSelectedCountry_Homepage,
  selectSelectedProvince_Homepage,
} from "src/app/store/location/location.selectors";
import {
  selectCurrentWeatherData,
  selectHistoricalStats,
  selectLoadingWeatherData,
  selectWeatherData,
  selectWeatherModalStatus,
} from "src/app/store/weather/weather.selectors";
import {
  loadCountries_Hompage,
  selectCountry_Homepage,
  selectProvince_Homepage,
} from "src/app/store/location/location.actions";
import {
  loadHistoricalStats,
  loadWeatherConditions,
  setCurrentWeather,
  updateWeatherModalStatus,
} from "src/app/store/weather/weather.actions";
import { cloneDeep } from "lodash-es";
import { ConfigService } from "src/app/services/config.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { EditWeatherDataComponent } from "../edit-weather-data/edit-weather-data.component";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit, AfterViewChecked {
  countriesList$ = this.store.select(selectCountriesList_Homepage);
  selectedCountry$ = this.store.select(selectSelectedCountry_Homepage);
  selectedProvince$ = this.store.select(selectSelectedProvince_Homepage);
  weatherForecastData$ = this.store.select(selectWeatherData);
  loadingCountriesList$ = this.store.select(
    selectLoadingCountriesList_Homepage
  );
  loadingWeatherData$ = this.store.select(selectLoadingWeatherData);
  historicalData$ = this.store.select(selectHistoricalStats);
  currentWeatherData$ = this.store.select(selectCurrentWeatherData);
  currentAccount$;
  displayWeatherModalStatus$ = this.store.select(selectWeatherModalStatus);

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

  constructor(
    private store: Store,
    private readonly changeDetectorRef: ChangeDetectorRef,
    private configService: ConfigService,
    private modalService: NzModalService
  ) {}
  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges(); // fix issue ExpressionChangedAfterItHasBeenCheckedError...
    this.currentAccount$ = this.configService.accountSubject;
  }

  ngOnInit(): void {
    this.store.dispatch(loadCountries_Hompage());
    this.store.dispatch(setCurrentWeather({ data: null }));
  }
  onChangeCountry(item: CountryDto) {
    localStorage.setItem("selectedCountryId", item.id.toString());
    localStorage.removeItem("selectedProvinceId");
    this.store.dispatch(setCurrentWeather({ data: null }));
    this.store.dispatch(selectCountry_Homepage({ selectedCountry: item }));
  }
  onChangeProvince(item: ProvinceDto) {
    localStorage.setItem("selectedProvinceId", item.id.toString());
    this.store.dispatch(setCurrentWeather({ data: null }));
    this.store.dispatch(selectProvince_Homepage({ selectedProvince: item }));
  }

  showTimezone(country: CountryDto) {
    return country.userDefined3 ?? moment.tz.guess();
  }
  showCurrentTime(country: CountryDto) {
    return moment(new Date().getTime())
      .tz(country.userDefined3 ?? moment.tz.guess())
      .format("LLL Z");
  }
  showDate(country: CountryDto, inputDt: number) {
    if (country == null) {
      return null;
    }
    return moment
      .unix(inputDt)
      .tz(country.userDefined3 ?? moment.tz.guess())
      .format("ddd, MMM D");
  }
  showTime(country: CountryDto, inputDt: number) {
    if (country == null) {
      return null;
    }
    return moment
      .unix(inputDt)
      .tz(country.userDefined3 ?? moment.tz.guess())
      .format("hh:mmA");
  }
  loadWeatherDetail(wfd: DailyForecastWeatherDto, currentDt: number) {
    this.store.dispatch(setCurrentWeather({ data: wfd }));
    if (wfd.dt !== currentDt) {
      setTimeout(() => {
        document.getElementById("box-scrollmenu").scrollLeft =
          document.getElementById("dt-" + wfd.dt).getBoundingClientRect().left /
          3;
      }, 100);
      this.store.dispatch(
        loadHistoricalStats({
          currentWeatherData: wfd,
        })
      );
    }
  }

  cloneData(data) {
    return cloneDeep(data);
  }
  onShowHideWeatherModal(isShow: boolean) {
    this.store.dispatch(updateWeatherModalStatus({ status: isShow }));
  }
  handleOk(data): void {
    console.log(data);
  }

  handleCancel(): void {
    this.onShowHideWeatherModal(false);
  }
  showWeatherData(
    data: DailyForecastWeatherDto,
    country: CountryDto,
    province: ProvinceDto
  ) {
    this.store.dispatch(loadWeatherConditions());
    this.modalService.create({
      nzTitle: "Edit Weather Data",
      nzContent: EditWeatherDataComponent,
      nzComponentParams: {
        weatherData: data,
        country: country,
        province: province,
      },
    });
  }
}
