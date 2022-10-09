import { Injectable } from "@angular/core";
import { CanLoad, Route, UrlSegment, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { ConfigService } from "../services/config.service";

@Injectable({
  providedIn: "root",
})
export class CanLoadAdminGuard implements CanLoad {
  constructor(private configService: ConfigService) {}
  canLoad(
    route: Route,
    segments: UrlSegment[]
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    let currentAccount = this.configService.accountSubject.getValue();
    if (currentAccount.roles.indexOf("Administrator") > -1) {
      return true;
    }
    return false;
  }
}
