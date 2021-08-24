import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AuthTokenManagerService} from "../services/managers/auth-token-manager.service";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private authTokenManagerService: AuthTokenManagerService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    return next.handle(request);
  }
}
