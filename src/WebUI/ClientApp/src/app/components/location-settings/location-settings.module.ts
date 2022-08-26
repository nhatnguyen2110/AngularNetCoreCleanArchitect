import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LocationSettingsComponent } from "./location-settings.component";
import { LocationSettingsRoutingModule } from "./location-settings-routing.module";
import { SharedModule } from "src/app/shared/shared.module";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzI18nModule } from "ng-zorro-antd/i18n";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzTableModule } from "ng-zorro-antd/table";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzDividerModule } from "ng-zorro-antd/divider";
import { AddEditCountryModalComponent } from './add-edit-country-modal/add-edit-country-modal.component';
import { AddEditProvinceModalComponent } from './add-edit-province-modal/add-edit-province-modal.component';
import { NzAutocompleteModule } from "ng-zorro-antd/auto-complete";
import { NzModalModule } from "ng-zorro-antd/modal";
import { NzPopconfirmModule } from "ng-zorro-antd/popconfirm";

@NgModule({
  declarations: [LocationSettingsComponent, AddEditCountryModalComponent, AddEditProvinceModalComponent],
  imports: [
    CommonModule,
    LocationSettingsRoutingModule,
    SharedModule,
    NzButtonModule,
    NzCardModule,
    NzFormModule,
    NzI18nModule,
    NzIconModule,
    NzInputModule,
    NzLayoutModule,
    NzNotificationModule,
    NzSpinModule,
    NzSpaceModule,
    NzTableModule,
    NzDropDownModule,
    NzDividerModule,
    NzAutocompleteModule,
    NzModalModule,
    NzPopconfirmModule,
  ],
})
export class LocationSettingsModule {}
