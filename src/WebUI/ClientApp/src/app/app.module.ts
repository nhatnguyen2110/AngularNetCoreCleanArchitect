import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { LayoutComponent } from './layout/layout.component';
import { SliderMenuComponent } from './layout/slider-menu/slider-menu.component';
import { MainNgZorroAntdModule } from './ng-zorro-antd.module';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    SliderMenuComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    SharedModule,
    DashboardModule,
    MainNgZorroAntdModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
