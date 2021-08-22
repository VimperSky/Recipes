import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";
import {UserProfileInfoDto} from "../../../dto/user/user-profile-info-dto";
import {SetUserProfileInfoDto} from "../../../dto/user/set-user-profile-info-dto";

@Injectable()
export abstract class UserService {
  abstract register(register: Register): Observable<string>;

  abstract login(login: Login): Observable<string>;

  abstract getProfileInfo(): Observable<UserProfileInfoDto>;

  abstract setProfileInfo(setProfileInfo: SetUserProfileInfoDto): Observable<string>
}
