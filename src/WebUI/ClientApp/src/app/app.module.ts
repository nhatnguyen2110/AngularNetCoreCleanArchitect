import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { TokenComponent } from './token/token.component';
import { LocationSettingsComponent } from './location-settings/location-settings.component';
import { NotifierModule } from 'angular-notifier';
import { BlockUIModule } from "ng-block-ui";
import { BlockUIHttpModule } from "ng-block-ui/http";
import { DebouncePreventClickDirective } from './directives/debouncePreventClick.directive';
import { ThrottleTimePreventClickDirective } from './directives/throtteTimePreventClick.directive';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    TokenComponent,
    LocationSettingsComponent,
    DebouncePreventClickDirective,
    ThrottleTimePreventClickDirective,
    DashboardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NotifierModule,
    NgSelectModule,
    NgChartsModule,
    BlockUIModule.forRoot({
      delayStart: 0,
      delayStop: 0,
    }),
    // BlockUIHttpModule.forRoot({
    //   blockAllRequestsInProgress: true
    // }),
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
