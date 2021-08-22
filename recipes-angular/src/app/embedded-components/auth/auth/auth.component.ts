import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {RegisterComponent} from "../register/register.component";
import {LoginComponent} from "../login/login.component";

export interface AuthDialogData {
  text: string | undefined;
}

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['../../../shared-styles/form-styles.scss', '../../../shared-styles/dialog-styles.scss', './auth.component.scss']
})
export class AuthComponent {

  public text: string = "Добавлять рецепты могут только зарегистрированные пользователи.";

  public constructor(@Inject(MAT_DIALOG_DATA) public data: AuthDialogData,
                     private dialog: MatDialog,
                     private dialogRef: MatDialogRef<AuthComponent>) {
    if (data == null) {
      return;
    }

    if (data.text != null)
      this.text = data.text;
  }


  public logIn() {
    this.dialogRef.close();
    this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });
  }

  public register() {
    this.dialogRef.close();
    this.dialog.open(RegisterComponent, {
      panelClass: 'register-dialog-container'
    });
  }
}
