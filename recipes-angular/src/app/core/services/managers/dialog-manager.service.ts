import {Injectable} from '@angular/core';
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {ErrorComponent} from "../../../embedded-components/error/error.component";
import {AuthComponent} from "../../../embedded-components/auth/auth/auth.component";
import {LoginComponent} from "../../../embedded-components/auth/login/login.component";
import {MatSnackBar, MatSnackBarRef} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class DialogManagerService {
  private errorDialogRef: MatDialogRef<ErrorComponent> | undefined;

  private snackErrorDialogRef: MatSnackBarRef<any> | undefined;

  constructor(private dialog: MatDialog, private snackBar: MatSnackBar) {}

  public openErrorDialog(text: string) {
    this.errorDialogRef = this.dialog.open(ErrorComponent, {
      hasBackdrop: true,
      data: {
        errorText: text
      }
    });
  }

  public openSnackErrorDialog(text: string) {
    if (this.snackErrorDialogRef)
      return;

    this.snackErrorDialogRef = this.snackBar.open(text, undefined, {
      duration: 5000
    });
    this.snackErrorDialogRef.afterDismissed().subscribe(() => {
      this.snackErrorDialogRef = undefined;
    })
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
