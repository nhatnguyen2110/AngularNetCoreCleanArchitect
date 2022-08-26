import { Injectable } from "@angular/core";
import { createEffect, ofType, Actions } from "@ngrx/effects";
import { Guid } from "guid-typescript";
import { NzModalRef, NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import {
  catchError,
  exhaustMap,
  map,
  of,
  switchMap,
  tap,
  throwError,
} from "rxjs";
import { AddEditCountryModalComponent } from "src/app/components/location-settings/add-edit-country-modal/add-edit-country-modal.component";
import { AddEditProvinceModalComponent } from "src/app/components/location-settings/add-edit-province-modal/add-edit-province-modal.component";
import {
  CountriesClient,
  CountryDto,
  CreateCountryCommand,
  CreateProvinceCommand,
  ProvincesClient,
  UpdateCountryCommand,
  UpdateProvinceCommand,
} from "src/app/web-api-client";
import {
  deleteCountryFailure,
  deleteCountrySuccess,
  deleteProvinceFailure,
  deleteProvinceSuccess,
  loadCountries_Admin,
  loadCountries_Failure_Admin,
  loadCountries_Failure_Homepage,
  loadCountries_Success_Admin,
  loadCountries_Success_Homepage,
  LocationActions,
  saveCountryFailure,
  saveCountrySuccess,
  saveProvinceFailure,
  saveProvinceSuccess,
  selectCountry_Homepage,
  selectProvince_Homepage,
} from "./location.actions";

@Injectable()
export class LocationEffects {
  loadCountries_Homepage$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_LOADCOUNTRIES_HOMEPAGE),
      switchMap(() => {
        let requestId = Guid.create().toString();
        return this.countriesClient.getList(null, 1, 999, requestId).pipe(
          map((res) =>
            loadCountries_Success_Homepage({ countriesList: res.data.items })
          ),
          catchError((error) => {
            console.log(error.response);
            return of(
              loadCountries_Failure_Homepage({
                error: JSON.parse(error.response),
              })
            );
          })
        );
      })
    )
  );
  loadCountries_Homepage_success$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_LOADCOUNTRIES_SUCCESS_HOMEPAGE),
      switchMap(({ countriesList }) => {
        let selectedCountryId = localStorage.getItem("selectedCountryId");
        let countriesListRes: CountryDto[] = countriesList;
        let selectedCountry: CountryDto = countriesListRes[0];
        if (selectedCountryId) {
          selectedCountry = countriesListRes.find(
            (x) => x.id == parseInt(selectedCountryId)
          );
        }
        return of(selectCountry_Homepage({ selectedCountry }));
      })
    )
  );
  selectSelectCountry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SELECTCOUNTRY_HOMEPAGE),
      switchMap(({ selectedCountry }) => {
        let selectedProvinceId = localStorage.getItem("selectedProvinceId");

        let selectedCountryRes: CountryDto = selectedCountry;
        let selectedProvince = selectedCountryRes.provinces[0];
        if (selectedProvinceId) {
          selectedProvince = selectedCountryRes.provinces.find(
            (x) => x.id == parseInt(selectedProvinceId)
          );
        }
        return of(selectProvince_Homepage({ selectedProvince }));
      })
    )
  );
  loadCountries_Homepage_failure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_LOADCOUNTRIES_FAILURE_HOMPAGE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );

  loadCountries_Admin$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_LOADCOUNTRIES_ADMIN),
      switchMap(() => {
        let requestId = Guid.create().toString();
        return this.countriesClient.getList(null, 1, 999, requestId).pipe(
          map((res) =>
            loadCountries_Success_Admin({ countriesList: res.data.items })
          ),
          catchError((error) => {
            return of(
              loadCountries_Failure_Admin({
                error: JSON.parse(error.response),
              })
            );
          })
        );
      })
    )
  );
  loadCountries_Admin_failure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_LOADCOUNTRIES_FAILURE_ADMIN),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  showModalCountry_Admin$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.COUNTRY_SHOWMODAL_ADMIN),
        tap(({ country }) => {
          if (country) {
            let titleModal = "Create Country";
            if (country["id"] > 0) {
              titleModal = "Edit Country";
            }
            this.modalRef = this.modal.create({
              nzTitle: titleModal,
              nzContent: AddEditCountryModalComponent,
              nzComponentParams: {
                country: country,
              },
            });
          }
        })
      ),
    { dispatch: false }
  );

  saveCountry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SAVECOUNTRY),
      switchMap(({ country }) => {
        console.log(country);
        if (country["id"] > 0) {
          return this.countriesClient
            .update(UpdateCountryCommand.fromJS(country))
            .pipe(
              map((res) => saveCountrySuccess()),
              catchError((error) => {
                return of(
                  saveCountryFailure({
                    error: JSON.parse(error.response) ?? error,
                  })
                );
              })
            );
        } else {
          return this.countriesClient
            .create(CreateCountryCommand.fromJS(country))
            .pipe(
              map((res) => saveCountrySuccess()),
              catchError((error) => {
                return of(
                  saveCountryFailure({
                    error: JSON.parse(error.response) ?? error,
                  })
                );
              })
            );
        }
      })
    )
  );
  saveCountryFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_SAVECOUNTRY_FAILURE),
        tap(({ error }) => {
          console.log(error);
          this.message.error(
            error["title"],
            error["message"] ?? JSON.stringify(error["errors"])
          );
        })
      ),
    { dispatch: false }
  );
  saveCountrySuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SAVECOUNTRY_SUCCESS),
      exhaustMap(() => {
        this.modalRef.destroy();
        this.message.success("Success", "Save successfully");
        return of(loadCountries_Admin());
      })
    )
  );

  showModalProvince_Admin$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.PROVINCE_SHOWMODAL_ADMIN),
        tap(({ province }) => {
          if (province) {
            let titleModal = "Create Province";
            if (province["id"] > 0) {
              titleModal = "Edit Province";
            }
            this.modalRef = this.modal.create({
              nzTitle: titleModal,
              nzContent: AddEditProvinceModalComponent,
              nzComponentParams: {
                province: province,
              },
            });
          }
        })
      ),
    { dispatch: false }
  );
  saveProvince$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SAVEPROVINCE),
      switchMap(({ province }) => {
        if (province["id"] > 0) {
          return this.provincesClient
            .update(UpdateProvinceCommand.fromJS(province))
            .pipe(
              map((res) => saveProvinceSuccess()),
              catchError((error) => {
                return of(
                  saveProvinceFailure({
                    error: JSON.parse(error.response) ?? error,
                  })
                );
              })
            );
        } else {
          return this.provincesClient
            .create(CreateProvinceCommand.fromJS(province))
            .pipe(
              map((res) => saveProvinceSuccess()),
              catchError((error) => {
                return of(
                  saveProvinceFailure({
                    error: JSON.parse(error.response) ?? error,
                  })
                );
              })
            );
        }
      })
    )
  );
  saveProvinceFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_SAVEPROVINCE_FAILURE),
        tap(({ error }) => {
          console.log(error);
          this.message.error(
            error["title"],
            error["message"] ?? JSON.stringify(error["errors"])
          );
        })
      ),
    { dispatch: false }
  );
  saveProvinceSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_SAVEPROVINCE_SUCCESS),
      exhaustMap(() => {
        this.message.success("Success", "Save successfully");
        this.modalRef.destroy();
        return of(loadCountries_Admin());
      })
    )
  );
  deleteCountry$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_DELETECOUNTRY),
      switchMap(({ countryId }) =>
        this.countriesClient.delete(countryId).pipe(
          map((res) => deleteCountrySuccess()),
          catchError((error) => {
            console.log(error.response);
            return of(
              deleteCountryFailure({
                error: JSON.parse(error.response) ?? error,
              })
            );
          })
        )
      )
    )
  );
  deleteCountryFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_DELETECOUNTRY_FAILURE),
        tap(({ error }) => {
          console.log(error);
          this.message.error(
            error["title"],
            error["message"] ?? JSON.stringify(error["errors"])
          );
        })
      ),
    { dispatch: false }
  );
  deleteCountrySuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_DELETECOUNTRY_SUCCESS),
      exhaustMap(() => {
        this.message.success("Success", "Delete successfully");
        return of(loadCountries_Admin());
      })
    )
  );

  deleteProvince$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_DELETEPROVINCE),
      switchMap(({ provinceId }) =>
        this.provincesClient.delete(provinceId).pipe(
          map((res) => deleteProvinceSuccess()),
          catchError((error) => {
            return of(
              deleteProvinceFailure({
                error: JSON.parse(error.response) ?? error,
              })
            );
          })
        )
      )
    )
  );
  deleteProvinceFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(LocationActions.LOCATION_DELETEPROVINCE_FAILURE),
        tap(({ error }) => {
          console.log(error);
          this.message.error(
            error["title"],
            error["message"] ?? JSON.stringify(error["errors"])
          );
        })
      ),
    { dispatch: false }
  );
  deleteProvinceSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(LocationActions.LOCATION_DELETEPROVINCE_SUCCESS),
      exhaustMap(() => {
        this.message.success("Success", "Delete successfully");
        return of(loadCountries_Admin());
      })
    )
  );

  private modalRef: NzModalRef;
  constructor(
    private actions$: Actions,
    private countriesClient: CountriesClient,
    private message: NzNotificationService,
    private modal: NzModalService,
    private provincesClient: ProvincesClient
  ) {}
}
