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
import {AuthService} from "../../../core/services/abstract/auth.service";
import {Register} from "../../../core/dto/auth/register";
import {HttpErrorResponse} from "@angular/common/http";

declare type Errors = {
  [key: string]: string;
};


const loginErrors: Errors = {
  'takenLogin': "Логин занят. Выберите другой",
  'default': "Введите логин"
}

const passwordErrors: Errors = {
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
  styleUrls: ['../auth-styles.scss', './register.component.scss']
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
              private dialogRef: MatDialogRef<LoginComponent>,
              private authService: AuthService,
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
    const dialogRef = this.dialog.open(LoginComponent, {
      panelClass: 'login-dialog-container'
    });

  }

  register() {
    this.registerForm.markAllAsTouched();
    if (this.registerForm.valid) {
      let registerDto: Register = {login: this.login.value, password: this.password.value, name: this.name.value }
      this.authService.register(registerDto).subscribe(() => {
        this.dialogRef.close()
      }, ((error: HttpErrorResponse) => {
        if (error.status == 409) {
          this.login.setErrors({takenLogin: true})
        }
      }))
    }
  }
}
