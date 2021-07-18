import { Injectable } from '@angular/core';
import {RecipePreview} from "../models/recipe-preview";
import {RecipePage} from "../models/recipe-page";
import {RecipesService} from "./recipes.service";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RecipesManagerService {
  private pageCount: number = 0;
  private currentPage: number = 1;

  constructor(private recipesService: RecipesService) { }

  public recipeList: RecipePreview[] = [];

  public get hasMore(): boolean {
    return this.pageCount > this.currentPage;
  }

  public update (recipePage: RecipePage, clear: boolean = false) {
    if (clear) {
      this.recipeList = recipePage.recipes;
      this.currentPage = 1;
    }
    else {
      this.recipeList = this.recipeList.concat(recipePage.recipes);
    }

    this.pageCount = recipePage.pageCount;
  }

  public loadInitial() {
    this.recipesService.getRecipeList(1, environment.pageSize, null).subscribe(result => {
      this.update(result, true);
    });
  }

  public loadMore() {
    this.recipesService.getRecipeList(this.currentPage + 1, environment.pageSize, null).subscribe(result => {
      this.update(result);
      this.currentPage += 1;
    });
  }

  public search(searchString: string) {
    this.recipesService.getRecipeList(1, environment.pageSize, searchString).subscribe(result => {
      this.update(result, true);
    });
  }
}
