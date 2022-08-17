import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Actions, createEffect, ofType, concatLatestFrom } from "@ngrx/effects";
import { Store } from "@ngrx/store";
import { Guid } from "guid-typescript";
import { NzMessageService } from "ng-zorro-antd/message";
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
      mergeMap(({ userName, password, keepLogin, returnUrl }) => {
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
            })
          )
          .pipe(
            map((data) =>
              loginSuccess({ returnUrl: returnUrl, authInfo: data.data })
            ),
            catchError((error) => {
              //console.log(JSON.parse(error.response).message);
              return of(
                loginFailure({ error: JSON.parse(error.response).message })
              );
            })
          );
      })
    )
  );
  verificationLoginAction$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.VERIFICATIONLOGIN_SUBMIT),
      mergeMap(({ verifyCode, email, returnUrl }) => {
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
            })
          )
          .pipe(
            map((data) =>
              loginSuccess({ returnUrl: returnUrl, authInfo: data.data })
            ),
            catchError((error) => {
              //console.log(error.response);
              return of(
                loginFailure({ error: JSON.parse(error.response).message })
              );
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
              return of(
                sendCodeFail({ error: JSON.parse(error.response).message })
              );
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
          console.log(authInfo);
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
          this.message.error(error);
        })
      ),
    { dispatch: false }
  );
  sendCodeFailure$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.SENDCODE_FAIL),
        tap(({ error }) => {
          this.message.error(error);
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
    private store: Store,
    private router: Router,
    private accountClient: AccountClient,
    private message: NzMessageService,
    private configService: ConfigService
  ) {}
}
