import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "src/app/guards/auth.guard";
import { DashboardComponent } from "./dashboard.component";
import { EditWeatherDataComponent } from "./edit-weather-data/edit-weather-data.component";

const routes: Routes = [
    {
      path: 'home',
      component: DashboardComponent,
      children: [
        {
          path: "edit-weather",
          component: EditWeatherDataComponent,
          data: {
            breadcrumb: "Edit Weather"
          },
          canActivate: [AuthGuard]
        }
      ]
    },
  ];
  
  @NgModule({
    imports: [
      CommonModule,
      RouterModule.forChild(routes)
    ],
    declarations: [],
    exports: [RouterModule]
  })
  export class DashboardRoutingModule { }