import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LocationSettingsComponent } from "./location-settings.component";

const routes: Routes = [
  {
    path: "",
    component: LocationSettingsComponent,
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  declarations: [],
  exports: [RouterModule],
})
export class LocationSettingsRoutingModule {}
