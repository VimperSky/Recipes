import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {LoginComponent} from "../auth/login/login.component";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent implements OnInit {

  @Input()
  public name: string | null = null;

  constructor(public dialog: MatDialog, private authManager: AuthTokenManagerService) {
    this.name = authManager.name;

    authManager.authChanged.subscribe(_ => {
      this.name = authManager.name;
    })

  }

  ngOnInit(): void {

  }

  logIn() {
    if (this.dialog.openDialogs.length == 0) {
      this.dialog.open(LoginComponent, {
        panelClass: 'login-dialog-container'
      });
    }
  }

  logOut() {
    this.authManager.removeToken();
  }

}
