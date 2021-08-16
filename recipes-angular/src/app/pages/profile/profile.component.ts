import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {UserService} from "../../core/services/communication/abstract/user.service";
import {UserProfile} from "../../core/dto/user/user-profile";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['../../shared-styles/form-styles.scss', './profile.component.scss']
})
export class ProfileComponent implements OnInit {
  name = this.fb.control('', [Validators.required])
  login = this.fb.control('', [Validators.required]);
  password = this.fb.control('', [Validators.required, Validators.minLength(8)]);
  passwordPlaceHolder: string = "Введите ваш старый пароль или придумайте новый";

  bio = this.fb.control('', []);

  profileForm = this.fb.group({
    name: this.name,
    login: this.login,
    password: this.password,
    bio: this.bio
  })


  constructor(private fb: FormBuilder, private userService: UserService) {
    this.profileForm.disable();
  }

  ngOnInit(): void {
    this.userService.getProfile().subscribe((userProfile: UserProfile) => {
      this.name.setValue(userProfile.name);
      this.login.setValue(userProfile.login);
      this.bio.setValue(userProfile.bio);
    });
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


}
