import {Injectable} from '@angular/core';
import {UserService} from "../abstract/user.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";
import {UserProfileInfoDto} from "../../../dto/user/user-profile-info-dto";
import {SetUserProfileInfoDto} from "../../../dto/user/set-user-profile-info-dto";
import {HttpParamsBuilderService} from "../../tools/http-params-builder.service";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

const basePath: string = "/api/user"

@Injectable()
export class ApiUserService extends UserService {
  constructor(private http: HttpClient, private paramsBuilder: HttpParamsBuilderService) {
    super();
  }

  register(register: Register): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/register", register);
  }

  login(login: Login): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/login", login);
  }

  getProfileInfo(): Observable<UserProfileInfoDto> {
    return this.http.get<UserProfileInfoDto>(environment.backendUrl + basePath + "/profile", this.paramsBuilder.authOptions);
  }

  setProfileInfo(dto: SetUserProfileInfoDto): Observable<string> {
    return this.http.patch<string>(environment.backendUrl + basePath + "/profile", dto, this.paramsBuilder.authOptions);
  }
}
