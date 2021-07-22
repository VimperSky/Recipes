import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";


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

  password = new FormControl('', [
    Validators.required,
  ]);
  repeatPassword = new FormControl('', [
    Validators.required,
    Validators.pattern(this.password.value)
  ]);

  constructor(fb: FormBuilder) {
    this.registerForm = fb.group( {
      name: this.name,
      login: this.login,
      password: this.password,
      repeatPassword: this.repeatPassword
    })
  }

  ngOnInit(): void {
  }

  logIn() {

  }


  register() {

  }
}
