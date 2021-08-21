import { Component } from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";

@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.scss']
})
export class MainHeaderComponent {

  constructor(public authTokenManager: AuthTokenManagerService) { }

  addRecipe() {

  }

  logIn() {

  }
}
