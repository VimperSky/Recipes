import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";
import {UserProfile} from "../../../dto/user/user-profile";

@Injectable()
export abstract class UserService {
  abstract register(register: Register): Observable<string>;
  abstract login(login: Login): Observable<string>;
  abstract getProfile(): Observable<UserProfile>;
}
