import { Injectable } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {ErrorComponent} from "../../../embedded-components/error/error.component";
import {AuthComponent} from "../../../embedded-components/auth/auth/auth.component";

@Injectable({
  providedIn: 'root'
})
export class DialogDisplayService {
  constructor(private dialog: MatDialog) {}

  openErrorDialog(text: string) {
    this.dialog.open(ErrorComponent, {
      hasBackdrop: true,
      data: {
        errorText: text
      }
    });
  }

  openAuthDialog() {
    this.dialog.open(AuthComponent, {
      panelClass: 'auth-dialog-container'
    });
  }
}
