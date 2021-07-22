import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {LoginComponent} from "../auth/login/login.component";
import {RegisterComponent} from "../auth/register/register.component";

@Component({
  selector: 'app-auth-header',
  templateUrl: './auth-header.component.html',
  styleUrls: ['./auth-header.component.scss']
})
export class AuthHeaderComponent implements OnInit {

  @Input()
  name: string | undefined;

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {

  }

  logIn() {
    const dialogRef = this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });
    // const dialogRef = this.dialog.open(RegisterComponent, {
    //   panelClass: 'register-dialog-container'
    // });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  logOut() {
    this.name = "";
  }

}
