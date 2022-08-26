import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LoginComponent } from "./login.component";
import { LoginRoutingModule } from "./login-routing.module";
import { SharedModule } from "src/app/shared/shared.module";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCardModule } from "ng-zorro-antd/card";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzI18nModule } from "ng-zorro-antd/i18n";
import { NzIconModule } from "ng-zorro-antd/icon";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzNotificationModule } from "ng-zorro-antd/notification";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { NzSpinModule } from "ng-zorro-antd/spin";
import { NzTabsModule } from "ng-zorro-antd/tabs";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzTypographyModule } from "ng-zorro-antd/typography";
import { TimerComponent } from "./timer/timer.component";
import { RecaptchaModule, RecaptchaFormsModule } from "ng-recaptcha";

@NgModule({
  declarations: [LoginComponent, TimerComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
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
    NzTabsModule,
    NzSpaceModule,
    NzCheckboxModule,
    NzTypographyModule,
    RecaptchaModule,
    RecaptchaFormsModule,
  ],
})
export class LoginModule {}
