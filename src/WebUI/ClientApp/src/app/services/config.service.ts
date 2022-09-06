import { Injectable } from "@angular/core";
import {
  AccountClient,
  AccountDto,
  ConfigsDto,
  FacebookLoginCommand,
  SystemClient,
} from "src/app/web-api-client";
import {
  BehaviorSubject,
  catchError,
  EMPTY,
  exhaustMap,
  from,
  Observable,
  of,
  tap,
  concatMap,
  map,
} from "rxjs";
import { JSEncrypt } from "jsencrypt";
import { ActivatedRoute, Router } from "@angular/router";

declare const FB: any;

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
    private route: ActivatedRoute,
    private accountClient: AccountClient
  ) {
    this.accountSubject = new BehaviorSubject<AccountDto>(null);
  }
  loadConfigs() {
    return this.systemClient.getConfigs().pipe(
      tap((config) => {
        this.systemConfig = config.data;
        if (this.systemConfig.facebook_AppId) {
          // wait for facebook sdk to initialize before starting the angular app
          window["fbAsyncInit"] = function () {
            FB.init({
              appId: config.data.facebook_AppId,
              cookie: true,
              xfbml: true,
              version: config.data.facebook_AppVer,
            });
          };
          // load facebook sdk script
          (function (d, s, id) {
            var js,
              fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) {
              return;
            }
            js = d.createElement(s);
            js.id = id;
            js.src = "https://connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
          })(document, "script", "facebook-jssdk");
        }
      })
    );
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
    if (FB) {
      FB.getLoginStatus(({ authResponse }) => {
        if (authResponse) {
          // revoke app permissions to logout completely because FB.logout() doesn't remove FB cookie
          FB.api("/me/permissions", "delete", null, () => FB.logout());
        }
      });
    }

    this.clearAccessToken();
    this.accountSubject.next(null);
    this.router.navigate(["/login"]);
  }
  facebookLogin() {
    from(
      new Promise<any>((resolve) =>
        FB.login(resolve, { scope: "public_profile,email" })
      )
    )
      .pipe(
        concatMap(({ authResponse }) => {
          if (!authResponse) return EMPTY;
          return of(authResponse.accessToken);
        })
      )
      .pipe(concatMap((accessToken) => this.fbAuthenticate(accessToken)))
      .subscribe((res) => {
        // get return url from query parameters or default to home page
        const returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
        this.router.navigateByUrl(returnUrl);
        //console.log(res);
      });
  }
  fbAuthenticate(accessToken: string) {
    // authenticate with the api using a facebook access token,
    // on success the api returns an account object with a JWT auth token
    //console.log(accessToken);
    return this.accountClient
      .facebookLogin(FacebookLoginCommand.fromJS({ access_Token: accessToken }))
      .pipe(
        map((res) => {
          this.setAccessToken(res.data.accessToken);
          this.setCurrentAccount(res.data.account);
          return res;
        })
      );
  }
}
