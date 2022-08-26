import { Injectable } from "@angular/core";
import {
  AccountClient,
  AccountDto,
  ConfigsDto,
  SystemClient,
} from "src/app/web-api-client";
import {
  BehaviorSubject,
  catchError,
  exhaustMap,
  Observable,
  of,
  tap,
} from "rxjs";
import { JSEncrypt } from "jsencrypt";
import { Router } from "@angular/router";

export enum LocalKeys {
  ACCESSTOKEN = "accessToken",
}
@Injectable({
  providedIn: "root",
})
export class ConfigService {
  systemConfig: ConfigsDto;
  accountSubject: BehaviorSubject<AccountDto>;
  constructor(
    private systemClient: SystemClient,
    private router: Router,
    private accountClient: AccountClient
  ) {
    this.accountSubject = new BehaviorSubject<AccountDto>(null);
  }
  loadConfigs() {
    return this.systemClient
      .getConfigs()
      .pipe(tap((config) => (this.systemConfig = config.data)));
  }
  loadCurrentAccount() {
    if (this.getAccessToken()) {
      return this.accountClient.getProfile(null).pipe(
        tap((acc) => {
          this.accountSubject.next(acc.data);
        }),
        catchError((err) => {
          console.log(err);
          //clear token
          this.clearAccessToken();
          return of(err);
        })
      );
    }
    return of();
  }
  encrypt(text: string) {
    var $encrypt = new JSEncrypt();
    $encrypt.setPublicKey(this.systemConfig.publicKeyEncode);
    return $encrypt.encrypt(text).toString();
  }
  getAccessToken() {
    return localStorage.getItem(LocalKeys.ACCESSTOKEN);
  }
  setAccessToken(accessToken: string) {
    localStorage.setItem(LocalKeys.ACCESSTOKEN, accessToken);
  }
  clearAccessToken() {
    localStorage.removeItem(LocalKeys.ACCESSTOKEN);
  }
  setCurrentAccount(acc: AccountDto) {
    this.accountSubject.next(acc);
  }
  logout() {
    this.clearAccessToken();
    this.accountSubject.next(null);
    this.router.navigate(["/login"]);
  }
}
