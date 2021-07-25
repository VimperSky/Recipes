import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {RegisterComponent} from "../register/register.component";
import {AuthService} from "../../../core/services/abstract/auth.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Login} from "../../../core/dto/auth/login";
import {environment} from "../../../../environments/environment";
import {LocalAuthManagerService} from "../../../core/services/managers/local-auth-manager.service";


declare type Errors = {
  [key: string]: string;
};
const serverErrors: Errors = {
  'invalidLoginPassword': "Неверная пара логин-пароль.",
  'default': "Сервер вернул неопознанную ошибку."
}

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

  get hasError(): boolean {
    return this.loginForm.errors != null;
  }

  getErrorText(): string {
    for (const key of Object.keys(serverErrors))
      if (this.loginForm.hasError(key))
        return serverErrors[key];

    return serverErrors['default'];
  }

  constructor(private dialog: MatDialog,
              private dialogRef: MatDialogRef<LoginComponent>,
              private authService: AuthService,
              private authManager: LocalAuthManagerService,
              fb: FormBuilder) {
    this.loginForm = fb.group( {
      login: this.login,
      password: this.password
    })
  }

  ngOnInit(): void {
  }


  logIn() {
    this.loginForm.markAllAsTouched();
    if (this.loginForm.valid) {
      let loginDto: Login = {login: this.login.value, password: this.password.value }
      this.authService.login(loginDto).subscribe((token: string) => {
        this.authManager.setToken(token);
        this.dialogRef.close()
      }, ((error: HttpErrorResponse) => {
        if (error.status == 401) {
          this.loginForm.setErrors({invalidLoginPassword: true})
        }
      }))
    }
  }


  register() {
    this.dialogRef.close();
    const dialogRef = this.dialog.open(RegisterComponent, {
      panelClass: 'register-dialog-container'
    });
   }

}
