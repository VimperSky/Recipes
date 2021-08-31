import {Component} from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {Router} from "@angular/router";
import {DialogManagerService} from "../../core/services/managers/dialog-manager.service";

@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.scss']
})
export class MainHeaderComponent {

  constructor(public authTokenManager: AuthTokenManagerService,
              private router: Router,
              private dialogManagerService: DialogManagerService) {
  }

  addRecipe() {
    if (this.authTokenManager.isAuthorized) {
      this.router.navigate(["/recipe/new"]);
    } else {
      this.dialogManagerService.openAuthDialog("Добавлять рецепты могут только зарегистрированные пользователи.");
    }
  }

  logIn() {
    this.dialogManagerService.openLoginDialog();
  }
}
