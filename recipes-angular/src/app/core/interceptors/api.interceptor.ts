import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {AuthTokenManagerService} from "../services/managers/auth-token-manager.service";
import {catchError} from "rxjs/operators";
import {DialogManagerService} from "../services/managers/dialog-manager.service";


@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private authTokenManagerService: AuthTokenManagerService,
              private dialogManagerService: DialogManagerService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authTokenManagerService.tokenValue) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authTokenManagerService.tokenValue}`
        }
      });
    }

    return next.handle(request).pipe(catchError((error: HttpErrorResponse) => {
      if (error.status == 0) { // no server connection
        this.dialogManagerService.openSnackErrorDialog("Нет соединения с сервером!");
      }
      return throwError(error);
    }));
  }
}
