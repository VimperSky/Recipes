import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {RegisterComponent} from "../register/register.component";
import {LoginComponent} from "../login/login.component";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['../../../shared-styles/form-styles.scss', '../../../shared-styles/auth-styles.scss', './auth.component.scss']
})
export class AuthComponent implements OnInit {

  constructor(private dialog: MatDialog, private dialogRef: MatDialogRef<AuthComponent>) { }

  ngOnInit(): void {
  }


  logIn() {
    this.dialogRef.close();
    this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });
  }

  register() {
    this.dialogRef.close();
    this.dialog.open(RegisterComponent, {
      panelClass: 'register-dialog-container'
    });
  }
}
