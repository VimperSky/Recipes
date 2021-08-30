import {Component, Input} from '@angular/core';
import {RecipePreview} from "../../core/dto/recipe/recipe-preview";
import {environment} from "../../../environments/environment";
import {ActivityService} from "../../core/services/communication/abstract/activity.service";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";

const defaultImagePath: string = "../../../assets/images/default_recipe.jpg"

@Component({
  selector: 'app-recipe-preview',
  templateUrl: './recipe-preview.component.html',
  styleUrls: ['./recipe-preview.component.scss']
})
export class RecipePreviewComponent {

  @Input()
  public recipe!: RecipePreview;

  @Input()
  public isClickable: boolean = true;

  constructor(private activityService: ActivityService,
              private authManager: AuthTokenManagerService,
              private dialogDisplay: DialogDisplayService) {
  }

  public get likeIcon(): string {
    return this.recipe.isLiked ? 'favorite' : 'favorite_border';
  }

  public get starIcon(): string {
    return this.recipe.isStarred ? 'star' : 'star_border';
  }

  public get login(): string {
    if (this.recipe.author != null)
      return "@" + this.recipe.author.login;

    return "@admin";
  }

  public getImagePath(): string {
    if (this.recipe.imagePath) {
      return environment.backendUrl + "/" + this.recipe.imagePath;
    }
    return defaultImagePath;
  }

  public doStar(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    if (!this.authManager.isAuthorized) {
      this.dialogDisplay.openAuthDialog("Ставить звезды могут только авторизованные пользователи.")
      return;
    }

    if (this.recipe.isStarred) {
      this.activityService.deleteStar(this.recipe.id).subscribe(() => {
        this.recipe.isStarred = false;
        this.recipe.starsCount -= 1;
      });
    } else {
      this.activityService.setStar(this.recipe.id).subscribe(() => {
        this.recipe.isStarred = true;
        this.recipe.starsCount += 1;
      });
    }
  }

  public doLike(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    if (!this.authManager.isAuthorized) {
      this.dialogDisplay.openAuthDialog("Ставить лайки могут только авторизованные пользователи.")
      return;
    }

    if (this.recipe.isLiked) {
      this.activityService.deleteLike(this.recipe.id).subscribe(() => {
        this.recipe.isLiked = false;
        this.recipe.likesCount -= 1;
      });
    } else {
      this.activityService.setLike(this.recipe.id).subscribe(() => {
        this.recipe.isLiked = true;
        this.recipe.likesCount += 1;
      });
    }
  }
}
