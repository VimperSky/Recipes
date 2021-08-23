import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeDetail} from "../../core/dto/recipe/recipe-detail";
import {RecipeService} from "../../core/services/communication/abstract/recipe.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {HttpErrorResponse} from "@angular/common/http";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {ErrorHandlingService} from "../../core/services/tools/error-handling.service";
import {UserActivity} from "../../core/dto/user/user-activity";
import {UserActivityManagerService} from "../../core/services/managers/user-activity-manager.service";
import {ActivityService} from "../../core/services/communication/abstract/activity.service";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {

  public recipeDetail: RecipeDetail | undefined;

  constructor(private activatedRoute: ActivatedRoute,
              private recipeService: RecipeService,
              private snackBar: MatSnackBar,
              private dialogDisplayService: DialogDisplayService,
              private router: Router,
              private tokenService: AuthTokenManagerService,
              private activityService: ActivityService,
              private errorHandlingService: ErrorHandlingService) {
  }

  public get isAvailableToModify(): boolean {
    return this.tokenService.isAuthorized && this.recipeDetail?.author?.id == this.tokenService.userId;
  }

  public ngOnInit(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.recipeService.detail(id).subscribe(result => {
      this.recipeDetail = result;
    });

    this.tokenService.authChanged.subscribe((value: boolean) => {
      if (!this.recipeDetail) return;

      if (value) {
        this.activityService.getUserActivity().subscribe((activity: UserActivity) => {
          if (!this.recipeDetail) return;

          if (activity.likedRecipes.includes(this.recipeDetail.id)) {
            this.recipeDetail.isLiked = true;
          }
          if (activity.starredRecipes.includes(this.recipeDetail.id)) {
            this.recipeDetail.isStarred = true;
          }

        });
      }
      else {
        this.recipeDetail.isLiked = false;
        this.recipeDetail.isStarred = false;
      }
    })

  }

  public delete() {
    if (this.recipeDetail) {
      this.recipeService.delete(this.recipeDetail.id).subscribe(() => {
        this.snackBar.open('Рецепт был успешно удален!',
          'ОК', {
            duration: 5000
          });
        this.router.navigate(['recipes'])
      }, (error: HttpErrorResponse) => {
        this.errorHandlingService.openErrorDialog(error, "При удалении рецепта произошла неопознанная ошибка.")
      })
    }
  }
}
