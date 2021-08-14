import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['../../shared-styles/form-styles.scss', './profile.component.scss']
})
export class ProfileComponent implements OnInit {
  name = this.fb.control('', [Validators.required])
  login = this.fb.control('', [Validators.required]);
  password = this.fb.control('', [Validators.required, Validators.minLength(8)]);

  bio = this.fb.control('', []);

  profileForm = this.fb.group({
    name: this.name,
    login: this.login,
    password: this.password,
    bio: this.bio
  })

  constructor(private fb: FormBuilder) {
    this.profileForm.disable();
  }

  get isEditMode(): boolean {
    return !this.profileForm.disabled;
  }

  editProfile() {
    this.profileForm.enable();
  }

  applyChanges() {
    this.profileForm.markAllAsTouched();
    if (!this.profileForm.valid)
      return;

    this.profileForm.disable();
  }

  ngOnInit(): void {}
}
