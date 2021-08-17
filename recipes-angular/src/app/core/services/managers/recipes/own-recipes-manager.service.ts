import {Injectable} from '@angular/core';
import {RecipePreview} from "../../../dto/recipe/recipe-preview";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {RecipesService} from "../../communication/abstract/recipes.service";
import {environment} from "../../../../../environments/environment";
import {BaseRecipesManagerService} from "./base-recipes-manager.service";

@Injectable()
export class OwnRecipesManagerService extends BaseRecipesManagerService {
  private pageCount: number = 0;
  private currentPage: number = 1;

  private updateRecipeList(recipePage: RecipesPage, clear: boolean = false) {
    if (clear) {
      this.recipeList = recipePage.recipes;
      this.currentPage = 1;
    }
    else {
      this.recipeList = this.recipeList.concat(recipePage.recipes);
    }

    this.pageCount = recipePage.pageCount;
  }

  constructor(private recipesService: RecipesService) {
    super();
  }

  public recipeList: RecipePreview[] = [];

  public get hasMore(): boolean {
    return this.pageCount > this.currentPage;
  }

  public loadInitial() {
    this.recipesService.getMyRecipes(environment.pageSize, 1).subscribe(result => {
      this.updateRecipeList(result, true);
    });
  }

  public loadMore() {
    this.recipesService.getMyRecipes(environment.pageSize, this.currentPage + 1).subscribe(result => {
      this.updateRecipeList(result);
      this.currentPage += 1;
    });
  }
}
