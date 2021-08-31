import {Component, Input} from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogManagerService} from "../../core/services/managers/dialog-manager.service";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent {
  @Input()
  public name: string | null = null;

  constructor(private dialogManagerService: DialogManagerService, private authManager: AuthTokenManagerService) {
    this.name = authManager.name;

    authManager.authChanged.subscribe(_ => {
      this.name = authManager.name;
    })
  }

  public logIn() {
    this.dialogManagerService.openLoginDialog();
  }

  public logOut() {
    this.authManager.removeToken();
  }

}
