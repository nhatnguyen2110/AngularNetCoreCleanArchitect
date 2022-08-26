import { APP_INITIALIZER } from "@angular/core";
import { tap } from "rxjs";
import { ConfigService } from "./config.service";
import { ThemeService } from "./theme.service";

export const ThemeInitializerProvider = {
  provide: APP_INITIALIZER,
  useFactory: (themeService: ThemeService) => () => {
    return themeService.loadTheme();
  },
  deps: [ThemeService],
  multi: true,
};
export const ConfigInitializerProvider = {
  provide: APP_INITIALIZER,
  useFactory: (configService: ConfigService) => {
    return () => configService.loadConfigs();
  },
  deps: [ConfigService],
  multi: true,
};
export const AccountInitializerProvider = {
  provide: APP_INITIALIZER,
  useFactory: (configService: ConfigService) => {
    return () => configService.loadCurrentAccount();
  },
  deps: [ConfigService],
  multi: true,
};
