import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {RegisterComponent} from "../register/register.component";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../auth-styles.scss', './login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  login = new FormControl('', [
    Validators.required,
  ]);
  password = new FormControl('', [
    Validators.required
  ]);


  constructor(private dialog: MatDialog, private dialogRef: MatDialogRef<LoginComponent>, fb: FormBuilder) {
    this.loginForm = fb.group( {
      login: this.login,
      password: this.password
    })
  }

  ngOnInit(): void {
  }


  logIn() {

  }


  register() {
    this.dialogRef.close();
    const dialogRef = this.dialog.open(RegisterComponent, {
      panelClass: 'register-dialog-container'
    });
   }

}
