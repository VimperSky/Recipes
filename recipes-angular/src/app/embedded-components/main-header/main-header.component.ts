import { Component } from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {Router} from "@angular/router";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";

@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.scss']
})
export class MainHeaderComponent {

  constructor(public authTokenManager: AuthTokenManagerService,
              private router: Router,
              private dialogDisplayService: DialogDisplayService) { }

  addRecipe() {
    if (this.authTokenManager.isAuthorized) {
      this.router.navigate(["/recipe/new"]);
    }
    else {
      this.dialogDisplayService.openAuthDialog();
    }
  }

  logIn() {
    this.dialogDisplayService.openLoginDialog();
  }
}
