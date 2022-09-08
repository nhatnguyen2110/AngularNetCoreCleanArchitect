import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Store } from "@ngrx/store";
import { ConfigService } from "src/app/services/config.service";
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
import { ReCaptchaV3Service } from "ng-recaptcha";

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

  enableRecaptcha: boolean;
  googleSiteKey: string;
  googleRecaptchaVersion: string;
  enableFacebookLogin = false;
  enableGoogleLogin = false;
  enableTwitterLogin = false;

  debug = false;

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
    private activatedRoute: ActivatedRoute,
    private configService: ConfigService //private recaptchaV3Service: ReCaptchaV3Service
  ) {
    this.enableRecaptcha = configService.systemConfig.enableGoogleReCaptcha;
    this.googleSiteKey = configService.systemConfig.googleSiteKey;
    this.enableFacebookLogin = configService.systemConfig.facebook_AppId !== "";
    this.enableGoogleLogin = configService.systemConfig.google_ClientID !== "";

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

    if (this.enableRecaptcha) {
      this.validateForm.addControl(
        "recaptchaToken",
        new FormControl(null, Validators.required)
      );
      this.verificationForm.addControl(
        "recaptchaToken",
        new FormControl(null, Validators.required)
      );
    }
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
        recaptchaToken: this.enableRecaptcha
          ? this.validateForm.value.recaptchaToken
          : null,
      })
    );
    //reset recaptcha
    if (this.enableRecaptcha) {
      this.validateForm.controls.recaptchaToken.reset();
    }
  }
  onSubmitVerificationLogin() {
    let returnUrl = this.activatedRoute.snapshot.queryParamMap.get("returnUrl");
    this.store.dispatch(
      verificationLoginSubmit({
        email: this.verificationForm.value.email,
        verifyCode: this.verificationForm.value.verificationCode,
        returnUrl: returnUrl,
        recaptchaToken: this.enableRecaptcha
          ? this.verificationForm.value.recaptchaToken
          : null,
      })
    );
    //reset recaptcha
    if (this.enableRecaptcha) {
      this.verificationForm.controls.recaptchaToken.reset();
    }
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
    this.configService.facebookLogin();
  }
  onGoogleLogin() {
    this.configService.googleLogin();
  }
}
