import {Component, OnInit} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators
} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {LoginComponent} from "../login/login.component";
import {UserService} from "../../../core/services/communication/abstract/user.service";
import {Register} from "../../../core/dto/auth/register";
import {HttpErrorResponse} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ValidationProblemDetails} from "../../../core/dto/base/validation-problem-details";
import {ErrorHandlingService} from "../../../core/services/tools/error-handling.service";
import {AuthTokenManagerService} from "../../../core/services/managers/auth-token-manager.service";


const loginErrors: Record<string, string> = {
  'takenLogin': "Логин занят. Выберите другой",
  'default': "Введите логин"
}

const passwordErrors: Record<string, string> = {
  'minLength': 'Пароль слишком короткий',
  'mismatch': "Пароли не совпадают",
  'default': "Введите пароль"
};


const samePasswordsValidator: ValidatorFn = (control: AbstractControl):  ValidationErrors | null => {
  const pass = control.get('firstPassword')?.value;
  const confirmPass = control.get('secondPassword')?.value
  return pass === confirmPass ? null : { mismatch: true };
}

const minLengthValidator: ValidatorFn = (control: AbstractControl):  ValidationErrors | null => {
  const pass = control.get('firstPassword')!;
  return pass.hasError('minlength') ? {minLength: true} : null;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../../../shared-styles/form-styles.scss', '../../../shared-styles/dialog-styles.scss', './register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  name = new FormControl('', [
    Validators.required,
  ]);
  login = new FormControl('', [
    Validators.required,
  ]);

  passwordForm: FormGroup;
  password = new FormControl('', [
    Validators.required,
    Validators.minLength(8)
  ]);
  confirmPassword = new FormControl('', [
    Validators.required,
  ]);

  constructor(private dialog: MatDialog,
              private dialogRef: MatDialogRef<RegisterComponent>,
              private authService: UserService,
              private snackBar: MatSnackBar,
              private errorHandlingService: ErrorHandlingService,
              private tokenManagerService: AuthTokenManagerService,
              fb: FormBuilder) {
    this.passwordForm = fb.group({
      firstPassword: this.password,
      secondPassword: this.confirmPassword
    }, {validators: [samePasswordsValidator, minLengthValidator]});

    this.registerForm = fb.group( {
      name: this.name,
      login: this.login,
      password: this.passwordForm
    });
  }

  get passwordHasError(): boolean {
    return this.passwordForm.errors != null;
  }

  getPasswordErrorText(): string {
    for (const key of Object.keys(passwordErrors))
      if (this.passwordForm.hasError(key))
        return passwordErrors[key];

    return passwordErrors['default'];
  }

  get loginHasError(): boolean {
    return this.login.errors?.takenLogin;
  }

  getLoginErrorText(): string {
    for (const key of Object.keys(loginErrors))
      if (this.login.hasError(key))
        return loginErrors[key];

    return loginErrors['default'];
  }



  ngOnInit(): void {
  }

  logIn() {
    this.dialogRef.close();
    this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });
  }

  register() {
    this.registerForm.markAllAsTouched();
    if (this.registerForm.valid) {
      let registerDto: Register = {login: this.login.value, password: this.password.value, name: this.name.value }
      this.authService.register(registerDto).subscribe((token: string) => {
        this.tokenManagerService.setToken(token);
        this.dialogRef.close()
        this.snackBar.open('Регистрация прошла успешно!', 'ОК', {
          duration: 5000
        })
      }, ((error: HttpErrorResponse) => {
        if (error.status == 400) {

          let formControlsMap = new Map<string, AbstractControl>([
            ['Name', this.name],
            ['Login', this.login],
            ['Password', this.passwordForm]
          ]);

          this.errorHandlingService.setValidationErrors(error, formControlsMap);
        }
        else if (error.status == 409) {
          this.login.setErrors({takenLogin: true})
        }
      }))
    }
  }
}
