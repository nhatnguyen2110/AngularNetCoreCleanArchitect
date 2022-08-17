import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Store } from "@ngrx/store";
import {
  accountLoginSubmit,
  loginInitial,
  sendCodeStartCountdown,
  sendCodeStopCountdown,
  sendCodeSubmit,
  verificationLoginSubmit,
} from "src/app/store/auth/auth.actions";
import {
  selectCountdownInSeconds,
  selectCountdownStatus,
  selectLoginFormError,
  selectLoginFormLoading,
} from "src/app/store/auth/auth.selectors";
import { MyValidators } from "src/app/validators/ng-zorro.validator";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  validateForm!: FormGroup;
  verificationForm!: FormGroup;
  title = "My Application";
  passwordVisible = false;

  loading$ = this.store.select(selectLoginFormLoading);
  errorLoginForm$ = this.store.select(selectLoginFormError);
  countdownStatus$ = this.store.select(selectCountdownStatus);
  countdownInSeconds$ = this.store.select(selectCountdownInSeconds);

  // current locale is key of the nzAutoTips
  // if it is not found, it will be searched again with `default`
  autoTips: Record<string, Record<string, string>> = {
    // en: {
    //   required: 'Input is required'
    // },
    default: {
      required: "The input is required",
      email: "The input is not valid email",
    },
  };
  constructor(
    private fb: FormBuilder,
    private store: Store,
    private activatedRoute: ActivatedRoute
  ) {
    const { required, maxLength, minLength, email, mobile } = MyValidators;
    this.validateForm = this.fb.group({
      userName: [null, [required, email]],
      password: [null, [required]],
      keepLogin: [true],
    });
    this.verificationForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      verificationCode: [null, [Validators.required, Validators.minLength(6)]],
    });
  }

  ngOnInit(): void {
    this.store.dispatch(loginInitial());
  }
  onSubmitAccountLogin() {
    let returnUrl = this.activatedRoute.snapshot.queryParamMap.get("returnUrl");
    this.store.dispatch(
      accountLoginSubmit({
        userName: this.validateForm.value.userName,
        password: this.validateForm.value.password,
        keepLogin: this.validateForm.value.keepLogin,
        returnUrl: returnUrl,
      })
    );
  }
  onSubmitVerificationLogin() {
    let returnUrl = this.activatedRoute.snapshot.queryParamMap.get("returnUrl");
    this.store.dispatch(
      verificationLoginSubmit({
        email: this.verificationForm.value.email,
        verifyCode: this.verificationForm.value.verificationCode,
        returnUrl: returnUrl,
      })
    );
  }
  onSendCode() {
    this.store.dispatch(
      sendCodeSubmit({ email: this.verificationForm.value.email })
    );
  }
  onStopCountDown() {
    this.store.dispatch(sendCodeStopCountdown());
  }
  onFacebookLogin() {
    this.store.dispatch(sendCodeStartCountdown());
  }
  onGoogleLogin() {
    this.onStopCountDown();
  }
}
