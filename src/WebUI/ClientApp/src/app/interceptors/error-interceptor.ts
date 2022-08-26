import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { ConfigService } from "../services/config.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  excludedUrls: Array<string>;
  constructor(private configService: ConfigService) {
    this.excludedUrls = ["Account/GetProfile"];
  }
  private isBelongToExcludedUrls(requestUrl: string): boolean {
    let positionIndicator: string = "api/";
    let position = requestUrl.indexOf(positionIndicator);
    if (position > 0) {
      let destination: string = requestUrl.substr(
        position + positionIndicator.length
      );
      for (let address of this.excludedUrls) {
        if (new RegExp(address).test(destination)) {
          return true;
        }
      }
    }
    return false;
  }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (
          [401, 403].includes(err.status) &&
          !this.isBelongToExcludedUrls(request.url)
        ) {
          // auto logout if 401 or 403 response returned from api
          this.configService.logout();
        }
        console.log(err);
        return throwError(err);
      })
    );
  }
}
