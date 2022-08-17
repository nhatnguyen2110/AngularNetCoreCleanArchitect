import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { SharedModule } from "./shared/shared.module";
import { DashboardModule } from "./components/dashboard/dashboard.module";
import { LayoutComponent } from "./layout/layout.component";
import { SliderMenuComponent } from "./layout/slider-menu/slider-menu.component";
import {
  ConfigInitializerProvider,
  ThemeInitializerProvider,
} from "./services/app-initializer.service";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";

import { NzAvatarModule } from "ng-zorro-antd/avatar";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzBreadCrumbModule } from "ng-zorro-antd/breadcrumb";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzDatePickerModule } from "ng-zorro-antd/date-picker";
import { NzDescriptionsModule } from "ng-zorro-antd/descriptions";
import { NzDividerModule } from "ng-zorro-antd/divider";
import { NzDrawerModule } from "ng-zorro-antd/drawer";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzGridModule } from "ng-zorro-antd/grid";
import { NzI18nModule } from "ng-zorro-antd/i18n";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzImageModule } from "ng-zorro-antd/image";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzInputNumberModule } from "ng-zorro-antd/input-number";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { NzModalModule } from "ng-zorro-antd/modal";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzPageHeaderModule } from "ng-zorro-antd/page-header";
import { NzPaginationModule } from "ng-zorro-antd/pagination";
import { NzPopconfirmModule } from "ng-zorro-antd/popconfirm";
import { NzPopoverModule } from "ng-zorro-antd/popover";
import { NzResultModule } from "ng-zorro-antd/result";
import { NzSegmentedModule } from "ng-zorro-antd/segmented";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzSkeletonModule } from "ng-zorro-antd/skeleton";
import { NzSliderModule } from "ng-zorro-antd/slider";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzStatisticModule } from "ng-zorro-antd/statistic";
import { NzSwitchModule } from "ng-zorro-antd/switch";
import { NzTableModule } from "ng-zorro-antd/table";
import { NzTabsModule } from "ng-zorro-antd/tabs";
import { NzTypographyModule } from "ng-zorro-antd/typography";
import { NzBackTopModule } from "ng-zorro-antd/back-top";
import { StoreManagerModule } from "./store/store-manager.module";

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    SliderMenuComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    DashboardModule,

    NzAvatarModule,
    NzButtonModule,
    NzBreadCrumbModule,
    NzCardModule,
    NzDatePickerModule,
    NzDescriptionsModule,
    NzDividerModule,
    NzDrawerModule,
    NzDropDownModule,
    NzFormModule,
    NzGridModule,
    NzI18nModule,
    NzIconModule,
    NzImageModule,
    NzInputModule,
    NzInputNumberModule,
    NzLayoutModule,
    NzMenuModule,
    NzModalModule,
    NzNotificationModule,
    NzPageHeaderModule,
    NzPaginationModule,
    NzPopconfirmModule,
    NzPopoverModule,
    NzResultModule,
    NzSegmentedModule,
    NzSelectModule,
    NzSkeletonModule,
    NzSliderModule,
    NzSpinModule,
    NzStatisticModule,
    NzSwitchModule,
    NzTableModule,
    NzTabsModule,
    NzTypographyModule,
    NzSpaceModule,
    NzBackTopModule,

    StoreManagerModule,
  ],
  providers: [ConfigInitializerProvider, ThemeInitializerProvider],
  bootstrap: [AppComponent],
})
export class AppModule {}
