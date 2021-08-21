import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {LoginComponent} from "../auth/login/login.component";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent implements OnInit {

  constructor(private dialogDisplay: DialogDisplayService, private authManager: AuthTokenManagerService) {
    this.name = authManager.name;

    authManager.authChanged.subscribe(_ => {
      this.name = authManager.name;
    })
  }

  @Input()
  public name: string | null = null;

  public ngOnInit(): void {}

  public logIn() {
    this.dialogDisplay.openLoginDialog();
  }

  public logOut() {
    this.authManager.removeToken();
  }

}
