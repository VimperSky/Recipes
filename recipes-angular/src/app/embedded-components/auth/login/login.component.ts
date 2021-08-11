import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {RegisterComponent} from "../register/register.component";
import {AuthService} from "../../../core/services/communication/abstract/auth.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Login} from "../../../core/dto/auth/login";
import {AuthTokenManagerService} from "../../../core/services/managers/auth-token-manager.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorHandlingService} from "../../../core/services/tools/error-handling.service";

const serverErrors: Record<string, string> = {
  'invalidLoginPassword': "Неверная пара логин-пароль.",
  'default': "Сервер вернул неопознанную ошибку."
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../../shared-styles/form-styles.scss', '../../../shared-styles/dialog-styles.scss', './login.component.scss']
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
              private tokenManagerService: AuthTokenManagerService,
              private snackBar: MatSnackBar,
              private errorHandlingService: ErrorHandlingService,
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
        this.tokenManagerService.setToken(token);
        this.dialogRef.close()
        this.snackBar.open('Авторизация прошла успешно!', 'ОК', {
          duration: 3000
        })
      }, ((error: HttpErrorResponse) => {
        if (error.status == 400) {
          let formControlsMap = new Map<string, AbstractControl>([
            ['Login', this.login],
            ['Password', this.password]
          ]);

          this.errorHandlingService.setValidationErrors(error, formControlsMap);
        }
        else if (error.status == 401) {
          this.loginForm.setErrors({invalidLoginPassword: true})
        }
      }))
    }
  }


  register() {
    this.dialogRef.close();
    this.dialog.open(RegisterComponent, {
      panelClass: 'register-dialog-container'
    });
   }

}
