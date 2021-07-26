import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {LoginComponent} from "../auth/login/login.component";
import {environment} from "../../../environments/environment";
import {LocalAuthManagerService} from "../../core/services/managers/local-auth-manager.service";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent implements OnInit {

  @Input()
  public name: string | null = null;

  constructor(public dialog: MatDialog, private authManager: LocalAuthManagerService) {
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
