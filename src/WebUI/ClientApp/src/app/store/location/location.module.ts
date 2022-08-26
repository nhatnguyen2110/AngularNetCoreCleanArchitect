import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { EffectsModule } from "@ngrx/effects";
import { LocationEffects } from "./location.effects";
import { StoreModule } from "@ngrx/store";
import { locationFeatureKey, locationReducer } from "./location.reducer";

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    EffectsModule.forFeature([LocationEffects]),
    StoreModule.forFeature(locationFeatureKey, locationReducer),
  ],
})
export class LocationModule {}
