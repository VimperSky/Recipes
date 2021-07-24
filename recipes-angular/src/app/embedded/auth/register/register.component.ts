import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup, ValidationErrors, ValidatorFn,
  Validators
} from "@angular/forms";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {LoginComponent} from "../login/login.component";
import {AuthService} from "../../../core/services/abstract/auth.service";
import {Register} from "../../../core/dto/auth/register";


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
              private dialogRef: MatDialogRef<RegisterComponent>, fb: FormBuilder,
              private authService: AuthService) {

    this.passwordForm = fb.group({
      firstPassword: this.password,
      secondPassword: this.confirmPassword
    }, {validators: this.samePasswordsValidator});

    this.registerForm = fb.group( {
      name: this.name,
      login: this.login,
      password: this.passwordForm
    });
  }

  get passwordsHaveError(): boolean {
    return this.passwordForm.errors != null &&
      (this.password.hasError('minlength') || this.passwordForm.hasError('notSame'));
  }

  getPasswordErrorText(): string {
    if (this.password.hasError('minlength'))
      return "Пароль слишком короткий";

    if (this.passwordForm.hasError('notSame'))
      return "Пароли не совпадают";

    return "Укажите пароль"
  }

  isEmptyOrSpaces(str: string) {
    return str === null || str.match(/^ *$/) !== null;
  }

  samePasswordsValidator: ValidatorFn = (control: AbstractControl):  ValidationErrors | null => {
    const pass = control.get('firstPassword')?.value;
    const confirmPass = control.get('secondPassword')?.value
    return pass === confirmPass ? null : { notSame: true };
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
    alert (this.registerForm.valid)
    if (this.registerForm.valid) {
      let registerDto: Register = {login: this.login.value, passwordHash: this.password.value, name: this.name.value }
      this.authService.register(registerDto).subscribe((val: boolean) => {
        this.dialogRef.close()
      })
    }
  }
}
