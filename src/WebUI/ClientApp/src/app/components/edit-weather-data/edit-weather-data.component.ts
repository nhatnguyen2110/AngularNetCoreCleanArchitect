import {
  AfterViewChecked,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { NzModalRef } from "ng-zorro-antd/modal";
import { saveWeatherData } from "src/app/store/weather/weather.actions";
import {
  selectLoadingSaveWeatherData,
  selectWeatherConditions,
} from "src/app/store/weather/weather.selectors";
import {
  CountryDto,
  DailyForecastWeatherDto,
  OWPWeatherCondition,
  ProvinceDto,
} from "src/app/web-api-client";

@Component({
  selector: "app-edit-weather-data",
  templateUrl: "./edit-weather-data.component.html",
  styleUrls: ["./edit-weather-data.component.scss"],
})
export class EditWeatherDataComponent implements OnInit, AfterViewChecked {
  @Input() weatherData: DailyForecastWeatherDto;
  @Input() country: CountryDto;
  @Input() province: ProvinceDto;
  editWeatherForm: UntypedFormGroup;
  debug = false;
  weatherConditions$ = this.store.select(selectWeatherConditions); //WeatherConditionCollectionDto;
  loadingSaveWeatherData$ = this.store.select(selectLoadingSaveWeatherData);

  constructor(
    private modal: NzModalRef,
    private fb: UntypedFormBuilder,
    private store: Store,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {}
  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges(); // fix issue ExpressionChangedAfterItHasBeenCheckedError...
  }
  ngOnInit(): void {
    this.editWeatherForm = this.fb.group({
      id: 0,
      dt: 0,
      provinceId: 0,
      sunrise: 0,
      sunset: 0,
      temp_avg: [0, Validators.compose([Validators.required])],
      temp_min: [0, Validators.compose([Validators.required])],
      temp_max: [0, Validators.compose([Validators.required])],
      humidity: [
        0,
        Validators.compose([Validators.required, Validators.min(0)]),
      ],
      dew_point: [0, Validators.compose([Validators.required])],
      pop: [0, Validators.compose([Validators.required, Validators.min(0),Validators.max(1)])],
      wind_speed: [
        0,
        Validators.compose([Validators.required, Validators.min(0)]),
      ],
      clouds: [0, Validators.compose([Validators.required, Validators.min(0)])],
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
    this.editWeatherForm.patchValue({
      ...this.weatherData,
      provinceId: this.province.id,
    });
  }
  onSubmit() {
    //console.log(this.editWeatherForm.value);
    this.store.dispatch(saveWeatherData({data: this.editWeatherForm.value}));
  }
  destroyModal(): void {
    this.modal.destroy();
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
      weather_icon_url: $event.iconUrl,
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
}
