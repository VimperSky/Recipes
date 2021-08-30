import {Injectable} from '@angular/core';
import {RecipePreview} from "../../../dto/recipe/recipe-preview";
import {environment} from "../../../../../environments/environment";
import {RecipesService} from "../../communication/abstract/recipes.service";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {UserActivityDto} from "../../../dto/activity/user-activity-dto";
import {Observable} from "rxjs";
import {AuthTokenManagerService} from "../auth-token-manager.service";
import {ActivityService} from "../../communication/abstract/activity.service";
import {MyRecipesActivityDto} from "../../../dto/activity/my-recipes-activity-dto";

@Injectable()
export abstract class RecipesManagerService {
  public recipeList: RecipePreview[] = [];
  protected isPendingAction: boolean = false;
  private pageCount: number = 1;
  private currentPage: number = 1;

  public constructor(protected recipesService: RecipesService,
                     private activityService: ActivityService,
                     private authTokenManagerService: AuthTokenManagerService) {
  }

  public get hasMore(): boolean {
    return this.pageCount > this.currentPage;
  }

  public abstract getRecipes(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage>;

  public loadInitial() {
    if (this.isPendingAction)
      return;

    this.getRecipes(environment.pageSize, 1, null).subscribe(result => {
      this.updateRecipeList(result, true);
      this.subscribeToAuthenticationEvent();
    });

  };

  public loadMore() {
    if (this.isPendingAction)
      return;

    this.getRecipes(environment.pageSize, this.currentPage + 1, null).subscribe(result => {
      this.updateRecipeList(result, false);
      this.currentPage += 1;
    });

  };

  public updateRecipeList(recipePage: RecipesPage, clear: boolean = false) {
    if (clear) {
      this.recipeList = recipePage.recipes;
      this.currentPage = 1;
    } else {
      this.recipeList = this.recipeList.concat(recipePage.recipes);
    }

    this.pageCount = recipePage.pageCount;
    this.isPendingAction = false;
  }

  private subscribeToAuthenticationEvent() {
    this.authTokenManagerService.authChanged.subscribe((value: boolean) => {
      if (value) {
        const dto: MyRecipesActivityDto = {
          recipeIds: this.recipeList.map(x => x.id)
        }
        this.activityService.getUserActivity(dto).subscribe((activity: UserActivityDto) => {
          for (const recipe of this.recipeList) {
            if (activity.likedRecipes.includes(recipe.id)) {
              recipe.isLiked = true;
            }
            if (activity.starredRecipes.includes(recipe.id)) {
              recipe.isStarred = true;
            }
          }
        });
      } else {
        for (const recipe of this.recipeList) {
          recipe.isLiked = false;
          recipe.isStarred = false;
        }
      }
    })
  }
}
