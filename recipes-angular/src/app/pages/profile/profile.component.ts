import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, Validators} from "@angular/forms";
import {UserService} from "../../core/services/communication/abstract/user.service";
import {UserProfileInfoDto} from "../../core/dto/user/user-profile-info-dto";
import {SetUserProfileInfoDto} from "../../core/dto/user/set-user-profile-info-dto";
import {HttpErrorResponse} from "@angular/common/http";
import {ErrorHandlingService} from "../../core/services/tools/error-handling.service";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {RecipesManagerService} from "../../core/services/managers/recipes/recipes-manager.service";
import {OwnRecipesManagerService} from "../../core/services/managers/recipes/own-recipes-manager.service";

export const loginErrors: Record<string, string> = {
  'takenLogin': "Логин занят. Выберите другой",
  'default': "Введите логин"
}

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../../shared-styles/form-styles.scss', './profile.component.scss'],
    providers: [
      {
        provide: RecipesManagerService,
        useClass: OwnRecipesManagerService
      },
    ]
  }
)
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


  constructor(private fb: FormBuilder,
              private userService: UserService,
              private errorHandlingService: ErrorHandlingService,
              private tokenManagerService: AuthTokenManagerService) {
    this.profileForm.disable();
  }

  get loginHasError(): boolean {
    return this.login.errors?.takenLogin;
  }

  get isEditMode(): boolean {
    return !this.profileForm.disabled;
  }

  getLoginErrorText(): string {
    for (const key of Object.keys(loginErrors))
      if (this.login.hasError(key))
        return loginErrors[key];

    return loginErrors['default'];
  }

  ngOnInit(): void {
    this.userService.getProfileInfo().subscribe((userProfile: UserProfileInfoDto) => {
      this.name.setValue(userProfile.name);
      this.login.setValue(userProfile.login);
      this.bio.setValue(userProfile.bio);
    });
  }

  editProfile() {
    this.profileForm.enable();
  }

  applyChanges() {
    this.profileForm.markAllAsTouched();
    if (!this.profileForm.valid)
      return;

    const dto: SetUserProfileInfoDto = {
      login: this.login.value,
      password: this.password.value,
      name: this.name.value,
      bio: this.bio.value
    }
    this.userService.setProfileInfo(dto).subscribe((token: string) => {
      this.profileForm.disable();
      this.tokenManagerService.setToken(token);
    }, ((error: HttpErrorResponse) => {
      if (error.status == 400) {
        let formControlsMap = new Map<string, AbstractControl>([
          ['Name', this.name],
          ['Login', this.login],
          ['Password', this.password],
          ['Bio', this.bio]
        ]);
        this.errorHandlingService.setValidationErrors(error, formControlsMap);
      } else if (error.status == 409) {
        this.login.setErrors({takenLogin: true})
      }
    }))
  }


}
