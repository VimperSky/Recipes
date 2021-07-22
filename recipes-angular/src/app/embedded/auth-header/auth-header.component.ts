import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {AuthComponent} from "../auth/auth.component";
import {LoginComponent} from "../auth/login/login.component";

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
      height: '480px',
      width: '700px',
      panelClass: 'login-dialog-container'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  logOut() {
    this.name = "";
  }

}
