import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";
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
    children: [
      {
        path: "home",
        component: DashboardComponent,
        data: {
          breadcrumb: "Home",
        },
      },
    ],
  },
  {
    path: "login",
    loadChildren: () =>
      import("./components/login/login.module").then((m) => m.LoginModule),
  },
  {
    path: "**",
    component: PageNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
