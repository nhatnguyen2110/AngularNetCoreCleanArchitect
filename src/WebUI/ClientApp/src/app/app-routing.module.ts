import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { PageForbiddenComponent } from "./components/page-forbidden/page-forbidden.component";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";
import { TokenComponent } from "./components/token/token.component";
import { CanLoadAdminGuard } from "./guards/admin.guard";
import { AuthGuard } from "./guards/auth.guard";
import { LayoutComponent } from "./layout/layout.component";

export const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    redirectTo: "home",
  },
  {
    path: "",
    component: LayoutComponent,
    data: {
      breadcrumb: "Home",
    },
    children: [
      {
        path: "home",
        component: DashboardComponent,
      },
      {
        path: "location-settings",
        loadChildren: () =>
          import(
            "./components/location-settings/location-settings.module"
          ).then((m) => m.LocationSettingsModule),
        data: {
          breadcrumb: "Location Settings",
        },
        canActivate: [AuthGuard],
        canLoad: [CanLoadAdminGuard]
      },
      {
        path: "token",
        component: TokenComponent,
        data: {
          breadcrumb: "Token",
        },
        canActivate: [AuthGuard],
      },
    ],
  },
  {
    path: "login",
    loadChildren: () =>
      import("./components/login/login.module").then((m) => m.LoginModule),
  },
  {
    path: "page-forbidden",
    component: PageForbiddenComponent,
  },
  {
    path: "**",
    component: PageNotFoundComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
