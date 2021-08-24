import {Component, Input} from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent {
  @Input()
  public name: string | null = null;

  constructor(private dialogDisplay: DialogDisplayService, private authManager: AuthTokenManagerService) {
    this.name = authManager.name;

    authManager.authChanged.subscribe(_ => {
      this.name = authManager.name;
    })
  }

  public logIn() {
    this.dialogDisplay.openLoginDialog();
  }

  public logOut() {
    this.authManager.removeToken();
  }

}
