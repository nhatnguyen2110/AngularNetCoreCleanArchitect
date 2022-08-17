import { createAction, props } from "@ngrx/store";
import { SignInResultDto } from "src/app/web-api-client";

export enum AuthActions {
  LOGIN_INITIAL = "[Auth] Initial Login",
  ACCOUNTLOGIN_SUBMIT = "[Auth] Account Login Submit",
  VERIFICATIONLOGIN_SUBMIT = "[Auth] Verification Login Submit",
  LOGIN_SUCCESS = "[Auth] Login Success",
  LOGIN_FAILURE = "[Auth] Login Failure",
  SENDCODE_SUBMIT = "[Auth] Send Code Submit",
  SENDCODE_SUCCESS = "[Auth] Send Code Success",
  SENDCODE_FAIL = "[Auth] Send Code Fail",
  SENDCODE_STARTCOUNTDOWN = "[Auth] Send Code start countdown",
  SENDCODE_STOPCOUNTDOWN = "[Auth] Send Code stop countdown",
}

export const loginInitial = createAction(AuthActions.LOGIN_INITIAL);
export const accountLoginSubmit = createAction(
  AuthActions.ACCOUNTLOGIN_SUBMIT,
  props<{
    userName: string;
    password: string;
    keepLogin: boolean;
    returnUrl: string | null;
  }>()
);
export const verificationLoginSubmit = createAction(
  AuthActions.VERIFICATIONLOGIN_SUBMIT,
  props<{ email: string; verifyCode: string; returnUrl: string | null }>()
);
export const loginSuccess = createAction(
  AuthActions.LOGIN_SUCCESS,
  props<{ authInfo: SignInResultDto; returnUrl: string | null }>()
);
export const loginFailure = createAction(
  AuthActions.LOGIN_FAILURE,
  props<{ error: any }>()
);
export const sendCodeSubmit = createAction(
  AuthActions.SENDCODE_SUBMIT,
  props<{ email: string }>()
);
export const sendCodeSuccess = createAction(
  AuthActions.SENDCODE_SUCCESS,
  props<{ expireInSeconds: number }>()
);
export const sendCodeFail = createAction(
  AuthActions.SENDCODE_FAIL,
  props<{ error: any }>()
);
export const sendCodeStartCountdown = createAction(
  AuthActions.SENDCODE_STARTCOUNTDOWN
);
export const sendCodeStopCountdown = createAction(
  AuthActions.SENDCODE_STOPCOUNTDOWN
);
