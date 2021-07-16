import { Injectable } from '@angular/core';
import {RecipePreview} from "../models/recipe-preview";
import {RecipePage} from "../models/recipe-page";
import {RecipesService} from "./recipes.service";

@Injectable({
  providedIn: 'root'
})
export class RecipesManagerService {
  private hasMore: boolean = false;
  private loadPage: number = 2;

  constructor(private recipesService: RecipesService) { }

  public recipeList: RecipePreview[] = [];

  public get hasMoreValue(): boolean {
    return this.hasMore;
  }

  public update (recipePage: RecipePage, clear: boolean = false) {
    if (clear) {
      this.recipeList = recipePage.recipes;
      this.loadPage = 2;
    }
    else {
      this.recipeList = this.recipeList.concat(recipePage.recipes);
    }

    this.hasMore = recipePage.hasMore;
  }

  public loadInitial() {
    this.recipesService.getRecipeList(1, null).subscribe(result => {
      this.update(result, true);
    });
  }

  public loadMore() {
    this.recipesService.getRecipeList(this.loadPage, null).subscribe(result => {
      this.update(result);
      this.loadPage += 1;
    });
  }

  public search(searchString: string) {
    this.recipesService.getRecipeList(1, searchString).subscribe(result => {
      this.update(result, true);
    });
  }
}
