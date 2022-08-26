import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { EffectsModule } from "@ngrx/effects";
import { WeatherEffects } from "./weather.effects";
import { StoreModule } from "@ngrx/store";
import { weatherFeatureKey, weatherReducer } from "./weather.reducer";

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    EffectsModule.forFeature([WeatherEffects]),
    StoreModule.forFeature(weatherFeatureKey, weatherReducer),
  ],
})
export class WeatherModule {}
