import { AbstractControl, ValidatorFn, Validators } from "@angular/forms";
import { NzSafeAny } from "ng-zorro-antd/core/types";

// current locale is key of the MyErrorsOptions
export type MyErrorsOptions = { en: string } & Record<string, NzSafeAny>;
export type MyValidationErrors = Record<string, MyErrorsOptions>;

export class MyValidators extends Validators {
  static override minLength(minLength: number): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.minLength(minLength)(control) === null) {
        return null;
      }
      return {
        minlength: {
          //'zh-cn': `最小长度为 ${minLength}`,
          en: `Min Length is ${minLength}`,
        },
      };
    };
  }

  static override maxLength(maxLength: number): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.maxLength(maxLength)(control) === null) {
        return null;
      }
      return {
        maxlength: {
          //"zh-cn": `最大长度为 ${maxLength}`,
          en: `Max Length is ${maxLength}`,
        },
      };
    };
  }

  static mobile(control: AbstractControl): MyValidationErrors | null {
    const value = control.value;

    if (isEmptyInputValue(value)) {
      return null;
    }

    return isMobile(value)
      ? null
      : {
          mobile: {
            //"zh-cn": `手机号码格式不正确`,
            en: `Mobile phone number is not valid`,
          },
        };
  }
}

function isEmptyInputValue(value: NzSafeAny): boolean {
  return value == null || value.length === 0;
}

function isMobile(value: string): boolean {
  return typeof value === "string" && /(^1\d{10}$)/.test(value);
}
