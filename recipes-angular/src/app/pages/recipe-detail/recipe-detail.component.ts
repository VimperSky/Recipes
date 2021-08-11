import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeDetail} from "../../core/dto/recipe/recipe-detail";
import {RecipeService} from "../../core/services/communication/abstract/recipe.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Location} from "@angular/common";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {HttpErrorResponse} from "@angular/common/http";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {ProblemDetails} from "../../core/dto/base/problem-details";
import {ErrorHandlingService} from "../../core/services/tools/error-handling.service";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {

  recipeDetail: RecipeDetail | undefined;
  get isAvailableToModify(): boolean {
    return this.tokenService.isAuthorized && this.recipeDetail?.authorId == this.tokenService.userId;
  }

  constructor(private activatedRoute: ActivatedRoute,
              private recipeService: RecipeService,
              private snackBar: MatSnackBar,
              private dialogDisplayService: DialogDisplayService,
              private router: Router,
              private tokenService: AuthTokenManagerService,
              private errorHandlingService: ErrorHandlingService) {
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.recipeService.detail(id).subscribe(result => {
      this.recipeDetail = result;
    });
  }

  delete() {
    if (this.recipeDetail) {
      this.recipeService.delete(this.recipeDetail.id).subscribe(() => {
        this.snackBar.open('Рецепт был успешно удален!',
          'ОК', {duration: 5000
          });
        this.router.navigate(['recipes'])
      }, (error: HttpErrorResponse) => {
        this.errorHandlingService.openErrorDialog(error, "При удалении рецепта произошла неопознанная ошибка.")
      })
    }
  }
}
