import {Injectable} from '@angular/core';
import {AuthTokenManagerService} from "../managers/auth-token-manager.service";
import {HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class HttpParamsBuilderService {
  constructor(private tokenManager: AuthTokenManagerService) { }

  public get authOptions() {
    let newOptions = {headers: new HttpHeaders()};
    newOptions.headers = newOptions.headers.set('Authorization', 'Bearer ' + this.tokenManager.tokenValue);
    return newOptions;
  }
}
