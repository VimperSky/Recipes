import {Injectable} from '@angular/core';
import {RecipePreview} from "../../../dto/recipe/recipe-preview";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {RecipesService} from "../../communication/abstract/recipes.service";
import {environment} from "../../../../../environments/environment";
import {BaseRecipesManagerService} from "./base-recipes-manager.service";

@Injectable()
export class AllRecipesManagerService extends BaseRecipesManagerService {
  public recipeList: RecipePreview[] = [];
  private pageCount: number = 0;
  private currentPage: number = 1;
  private searchString: string | null = null;
  private isPendingAction: boolean = false;

  constructor(private recipesService: RecipesService) {
    super();
  }

  public get hasMore(): boolean {
    return this.pageCount > this.currentPage;
  }

  public loadInitial() {
    if (this.isPendingAction)
      return;

    this.isPendingAction = true;
    this.recipesService.getRecipeList(environment.pageSize, 1, null).subscribe(result => {
      this.updateRecipeList(result, true);
    });
  }

  public loadMore() {
    if (this.isPendingAction)
      return;

    this.isPendingAction = true;
    this.recipesService.getRecipeList(environment.pageSize, this.currentPage + 1, this.searchString).subscribe(result => {
      this.updateRecipeList(result);
      this.currentPage += 1;
    });
  }

  public search(searchString: string | null) {
    if (this.isPendingAction)
      return;
    if (searchString == "")
      searchString = null;

    this.isPendingAction = true;
    this.recipesService.getRecipeList(environment.pageSize, 1, searchString).subscribe(result => {
      this.updateRecipeList(result, true);
      this.searchString = searchString;
    });
  }

  private updateRecipeList(recipePage: RecipesPage, clear: boolean = false) {
    if (clear) {
      this.recipeList = recipePage.recipes;
      this.currentPage = 1;
    } else {
      this.recipeList = this.recipeList.concat(recipePage.recipes);
    }

    this.pageCount = recipePage.pageCount;
    this.isPendingAction = false;
  }
}
