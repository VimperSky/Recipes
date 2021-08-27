import {Injectable} from '@angular/core';
import {UserService} from "../abstract/user.service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";
import {UserProfileInfoDto} from "../../../dto/user/user-profile-info-dto";
import {SetUserProfileInfoDto} from "../../../dto/user/set-user-profile-info-dto";
import {UserStats} from "../../../dto/user/user-stats";

const basePath: string = "/api/user"

@Injectable()
export class ApiUserService extends UserService {
  constructor(private http: HttpClient) {
    super();
  }

  register(register: Register): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/register", register);
  }

  login(login: Login): Observable<string> {
    return this.http.post<string>(environment.backendUrl + basePath + "/login", login);
  }

  getProfileInfo(): Observable<UserProfileInfoDto> {
    return this.http.get<UserProfileInfoDto>(environment.backendUrl + basePath + "/profile");
  }

  setProfileInfo(dto: SetUserProfileInfoDto): Observable<string> {
    return this.http.patch<string>(environment.backendUrl + basePath + "/profile", dto);
  }

  getUserStats(): Observable<UserStats> {
    return this.http.get<UserStats>(environment.backendUrl + basePath + "/stats");
  }

  validateCredentials(): Observable<void> {
    return this.http.get<void>(environment.backendUrl + basePath + "/validateCredentials");
  }
}
