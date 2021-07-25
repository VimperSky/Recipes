import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {Register} from "../../dto/auth/register";

@Injectable()
export abstract class AuthService {
  abstract register(register: Register): Observable<void>;
  abstract auth(): void;
}
