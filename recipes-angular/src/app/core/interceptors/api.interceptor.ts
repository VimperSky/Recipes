import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthTokenManagerService} from "../services/managers/auth-token-manager.service";


@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private authTokenManagerService: AuthTokenManagerService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authTokenManagerService.tokenValue) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authTokenManagerService.tokenValue}`
        }
      });
    }

    return next.handle(request);
  }
}
