import { Injectable } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {ErrorComponent} from "../../../embedded-components/error/error.component";
import {AuthComponent} from "../../../embedded-components/auth/auth/auth.component";
import {LoginComponent} from "../../../embedded-components/auth/login/login.component";

@Injectable({
  providedIn: 'root'
})
export class DialogDisplayService {
  constructor(private dialog: MatDialog) {}

  public openErrorDialog(text: string) {
    this.dialog.open(ErrorComponent, {
      hasBackdrop: true,
      data: {
        errorText: text
      }
    });
  }

  public openAuthDialog(text: string | undefined = undefined) {
    if (this.dialog.openDialogs.length > 0)
      return;

    this.dialog.open(AuthComponent, {
      panelClass: 'auth-dialog-container',
      data: {
        text: text
      }
    });
  }

  public openLoginDialog() {
    if (this.dialog.openDialogs.length > 0)
      return;

    this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });
  }
}
