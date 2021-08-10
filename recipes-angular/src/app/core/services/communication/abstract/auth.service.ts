import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {Register} from "../../../dto/auth/register";
import {Login} from "../../../dto/auth/login";

@Injectable()
export abstract class AuthService {
  abstract register(register: Register): Observable<string>;
  abstract login(login: Login): Observable<string>;
}
