import {Injectable} from '@angular/core';
import {UserService} from "../abstract/user.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";
import {UserProfile} from "../../../dto/user/user-profile";
import {AuthTokenManagerService} from "../../managers/auth-token-manager.service";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

const basePath: string = "/api/user"

@Injectable()
export class ApiUserService extends UserService {
  get optionsWithAuth() {
    let newOptions = {headers: new HttpHeaders()};
    newOptions.headers = newOptions.headers.set('Authorization', 'Bearer ' + this.tokenManager.tokenValue);
    return newOptions;
  }

  constructor(private http: HttpClient, private tokenManager: AuthTokenManagerService) {
    super();
  }

  register(register: Register): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/register", register);
  }

  login(login: Login): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/login", login);
  }

  getProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(environment.backendUrl + basePath + "/profile", this.optionsWithAuth);
  }
}
