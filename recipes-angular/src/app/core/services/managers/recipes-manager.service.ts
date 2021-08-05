import {Injectable} from '@angular/core';
import {RecipePreview} from "../../dto/recipe/recipe-preview";
import {RecipesPage} from "../../dto/recipe/recipes-page";
import {RecipesService} from "../abstract/recipes.service";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RecipesManagerService {
  private pageCount: number = 0;
  private currentPage: number = 1;
  private searchString: string | null = null;

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

  constructor(private recipesService: RecipesService) { }

  public recipeList: RecipePreview[] = [];

  public get hasMore(): boolean {
    return this.pageCount > this.currentPage;
  }
  public loadInitial() {
    this.recipesService.getRecipeList(environment.pageSize, 1, null).subscribe(result => {
      this.updateRecipeList(result, true);
    });
  }

  public loadMore() {
    this.recipesService.getRecipeList(environment.pageSize, this.currentPage + 1, this.searchString).subscribe(result => {
      this.updateRecipeList(result);
      this.currentPage += 1;
    });
  }

  public search(searchString: string | null) {
    if (searchString == "")
      searchString = null;
    this.recipesService.getRecipeList(environment.pageSize, 1, searchString).subscribe(result => {
      this.updateRecipeList(result, true);
      this.searchString = searchString;
    });
  }
}
