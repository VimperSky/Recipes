import {Injectable, OnInit} from '@angular/core';
import {environment} from "../../../../environments/environment";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Subject} from "rxjs";
import {Token} from "../../dto/auth/token";
import {UserService} from "../communication/abstract/user.service";

@Injectable({
  providedIn: 'root'
})
export class AuthTokenManagerService {
  private authChangeSub = new Subject<boolean>()
  private token: Token | null = null;

  public authChanged = this.authChangeSub.asObservable();

  constructor(private jwtHelper: JwtHelperService) {
    const token = localStorage.getItem(environment.jwtToken)
    if (token == null || this.jwtHelper.isTokenExpired(token))
      return;
    this.token = this.jwtHelper.decodeToken<Token>(token);
    this.token.raw = token;
  }

  public get isAuthorized(): boolean {
    return this.token != null;
  }

  public get name(): string | null {
    return this.token ? this.token.name : null;
  }

  public get tokenValue(): string | null {
    return this.token ? this.token.raw : null;
  }

  public get userId(): number | null {
    return this.token ? parseInt(this.token.userId) : null;
  }

  public removeToken() {
    localStorage.removeItem(environment.jwtToken);
    this.token = null;

    this.raiseTokenChange();
  }

  public setToken(token: string) {
    localStorage.setItem(environment.jwtToken, token);
    this.token = this.jwtHelper.decodeToken<Token>(token);
    this.token.raw = token;

    this.raiseTokenChange();
  }

  private raiseTokenChange() {
    this.authChangeSub.next(this.isAuthorized);
  }

}
