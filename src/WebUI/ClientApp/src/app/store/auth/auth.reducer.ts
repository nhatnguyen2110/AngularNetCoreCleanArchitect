import { createReducer, on } from "@ngrx/store";
import {
  loginInitial,
  loginFailure,
  loginSuccess,
  accountLoginSubmit,
  verificationLoginSubmit,
  sendCodeSubmit,
  sendCodeSuccess,
  sendCodeFail,
  sendCodeStartCountdown,
  sendCodeStopCountdown,
} from "./auth.actions";

export const authFeatureKey = "auth";
export interface AuthState {
  loading: boolean;
  error: string | null;
  countDownSendCodeInSeconds: number;
  startCountdown: boolean;
}

export const initialAuthState: AuthState = {
  error: null,
  loading: false,
  startCountdown: false,
  countDownSendCodeInSeconds: 0,
};

export const authReducer = createReducer(
  initialAuthState,
  on(loginInitial, (state) => {
    return {
      ...state,
      loading: false,
      error: null,
      countDownSendCodeInSeconds: 0,
      startCountdown: false,
    };
  }),
  on(accountLoginSubmit, (state) => {
    return { ...state, loading: true };
  }),
  on(verificationLoginSubmit, (state) => {
    return { ...state, loading: true };
  }),
  on(loginSuccess, (state) => {
    return { ...state, loading: false, error: null };
  }),
  on(loginFailure, (state, { error }) => {
    return { ...state, loading: false, error: error };
  }),
  on(sendCodeSubmit, (state) => {
    return { ...state, loading: true };
  }),
  on(sendCodeSuccess, (state, { expireInSeconds }) => {
    return {
      ...state,
      loading: false,
      error: null,
      countDownSendCodeInSeconds: expireInSeconds,
    };
  }),
  on(sendCodeFail, (state, { error }) => {
    return { ...state, error: error, loading: false };
  }),
  on(sendCodeStartCountdown, (state) => {
    return {
      ...state,
      startCountdown: true,
    };
  }),
  on(sendCodeStopCountdown, (state) => {
    return { ...state, startCountdown: false };
  })
);
