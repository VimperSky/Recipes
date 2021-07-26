import { Injectable } from '@angular/core';
import {environment} from "../../../../environments/environment";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Subject} from "rxjs";
import {Token} from "../../dto/auth/token";

@Injectable({
  providedIn: 'root'
})
export class LocalAuthManagerService {
  private authChangeSub = new Subject<void>()
  private token: Token | null = null;

  constructor(private jwtHelper: JwtHelperService) {
    let token = localStorage.getItem(environment.jwtToken)
    if (token == null || jwtHelper.isTokenExpired(token))
      return;

    this.token = this.jwtHelper.decodeToken<Token>(token);
  }

  private raiseTokenChange () {
    this.authChangeSub.next();
  }

  // public methods
  public authChanged = this.authChangeSub.asObservable();

  public get name(): string | null {
    return this.token ? this.token.name : null;
  }

  public removeToken() {
    localStorage.removeItem(environment.jwtToken);
    this.token = null;

    this.raiseTokenChange();
  }

  public setToken(token: string) {
    localStorage.setItem(environment.jwtToken, token);
    this.token = this.jwtHelper.decodeToken<Token>(token);

    this.raiseTokenChange();
  }

}
