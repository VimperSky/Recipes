import { Injectable } from '@angular/core';
import {AuthService} from "../abstract/auth.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {Observable} from "rxjs";
import {Register} from "../../dto/auth/register";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

const basePath: string = "/api/auth"

@Injectable({
  providedIn: 'root'
})
export class ApiAuthService extends AuthService {

  constructor(private http: HttpClient) {
    super();
  }

  auth(): void {
  }

  register(register: Register): Observable<void> {
    return this.http.post<void>(environment.backendUrl + basePath + "/register", register, httpOptions);
  }
}
