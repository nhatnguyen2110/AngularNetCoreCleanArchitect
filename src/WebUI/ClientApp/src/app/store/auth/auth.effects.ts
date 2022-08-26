import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { Guid } from "guid-typescript";
import { NzNotificationService } from "ng-zorro-antd/notification";
import {
  catchError,
  exhaustMap,
  map,
  mergeMap,
  of,
  tap,
  withLatestFrom,
} from "rxjs";
import { ConfigService } from "src/app/services/config.service";
import {
  AccountClient,
  GetEmailVerificationCodeCommand,
  SignInByEmailVerificationCodeCommand,
  SignInCommand,
} from "src/app/web-api-client";
import {
  AuthActions,
  loginFailure,
  loginSuccess,
  sendCodeFail,
  sendCodeStartCountdown,
  sendCodeSuccess,
} from "./auth.actions";

@Injectable()
export class AuthEffects {
  accountLoginAction$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.ACCOUNTLOGIN_SUBMIT),
      mergeMap(
        ({ userName, password, keepLogin, returnUrl, recaptchaToken }) => {
          let requestId = Guid.create().toString();
          //encrypt
          let _userName: string = userName;
          let _password: string = password;
          if (this.configService.systemConfig.enableEncryptAuthorize) {
            _userName = this.configService.encrypt(userName);
            _password = this.configService.encrypt(password);
          }
          return this.accountClient
            .signIn(
              SignInCommand.fromJS({
                email: _userName,
                password: _password,
                keepLogin: keepLogin,
                requestId: requestId,
                recaptchaToken: recaptchaToken,
              })
            )
            .pipe(
              map((data) =>
                loginSuccess({ returnUrl: returnUrl, authInfo: data.data })
              ),
              catchError((error) => {
                return of(loginFailure({ error: JSON.parse(error.response) }));
              })
            );
        }
      )
    )
  );
  verificationLoginAction$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.VERIFICATIONLOGIN_SUBMIT),
      mergeMap(({ verifyCode, email, returnUrl, recaptchaToken }) => {
        let requestId = Guid.create().toString();
        //encrypt
        let _email: string = email;
        let _code: string = verifyCode;
        if (this.configService.systemConfig.enableEncryptAuthorize) {
          _email = this.configService.encrypt(email);
          _code = this.configService.encrypt(verifyCode);
        }
        return this.accountClient
          .signInByEmailVerificationCode(
            SignInByEmailVerificationCodeCommand.fromJS({
              email: _email,
              code: _code,
              requestId: requestId,
              recaptchaToken: recaptchaToken,
            })
          )
          .pipe(
            map((data) =>
              loginSuccess({ returnUrl: returnUrl, authInfo: data.data })
            ),
            catchError((error) => {
              //console.log(error.response);
              return of(loginFailure({ error: JSON.parse(error.response) }));
            })
          );
      })
    )
  );
  sendCodeSubmit$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SENDCODE_SUBMIT),
      mergeMap(({ email }) => {
        let requestId = Guid.create().toString();
        return this.accountClient
          .getEmailVerificationCode(
            GetEmailVerificationCodeCommand.fromJS({
              email: email,
              requestId: requestId,
            })
          )
          .pipe(
            map((data) =>
              sendCodeSuccess({ expireInSeconds: data.data?.expireInSeconds })
            ),
            catchError((error) => {
              //console.log(error.response);
              return of(sendCodeFail({ error: JSON.parse(error.response) }));
            })
          );
      })
    )
  );
  loginSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.LOGIN_SUCCESS),
        tap(({ returnUrl, authInfo }) => {
          //store token + account info
          this.configService.setAccessToken(authInfo["accessToken"]);
          this.configService.setCurrentAccount(authInfo["account"]);
          //console.log(authInfo);
          if (returnUrl) {
            this.router.navigate([returnUrl]);
          } else {
            this.router.navigate(["home"]);
          }
        })
      ),
    { dispatch: false }
  );
  loginFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.LOGIN_FAILURE),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  sendCodeFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.SENDCODE_FAIL),
        tap(({ error }) => {
          this.message.error(error["title"], error["message"]);
        })
      ),
    { dispatch: false }
  );
  sendCodeSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SENDCODE_SUCCESS),
      mergeMap(() => of(sendCodeStartCountdown()))
    )
  );
  constructor(
    private actions$: Actions,
    private router: Router,
    private accountClient: AccountClient,
    private message: NzNotificationService,
    private configService: ConfigService
  ) {}
}
