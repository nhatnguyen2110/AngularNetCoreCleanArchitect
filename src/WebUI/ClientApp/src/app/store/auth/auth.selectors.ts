import { createFeatureSelector, createSelector } from "@ngrx/store";
import { authFeatureKey, AuthState } from "./auth.reducer";

export const selectAuthState = createFeatureSelector<AuthState>(authFeatureKey);
export const selectLoginFormLoading = createSelector(
  selectAuthState,
  (state: AuthState) => {
    {
      return state.loading;
    }
  }
);
export const selectLoginFormError = createSelector(
  selectAuthState,
  (state: AuthState) => {
    {
      return state.error;
    }
  }
);
export const selectCountdownStatus = createSelector(
  selectAuthState,
  (state: AuthState) => {
    {
      return state.startCountdown;
    }
  }
);
export const selectCountdownInSeconds = createSelector(
  selectAuthState,
  (state: AuthState) => {
    {
      return state.countDownSendCodeInSeconds;
    }
  }
);
