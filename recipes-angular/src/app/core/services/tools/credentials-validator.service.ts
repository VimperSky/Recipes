import {Injectable} from '@angular/core';
import {UserService} from "../communication/abstract/user.service";
import {AuthTokenManagerService} from "../managers/auth-token-manager.service";

@Injectable({
  providedIn: 'root'
})
export class CredentialsValidatorService {

  constructor(private authTokenManager: AuthTokenManagerService,
              private userService: UserService) {
  }

  public validate() {
    if (!this.authTokenManager.isAuthorized)
      return;

    this.userService.validateCredentials().subscribe(() => {
    }, () => {
      this.authTokenManager.removeToken()
    });
  }
}
