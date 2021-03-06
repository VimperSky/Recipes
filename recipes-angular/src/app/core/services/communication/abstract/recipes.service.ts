import {Injectable} from '@angular/core';
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {Observable} from "rxjs";
import {RecipePreview} from "../../../dto/recipe/recipe-preview";

@Injectable()
export abstract class RecipesService {
  abstract getRecipeList(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage>;

  abstract getMyRecipes(pageSize: number, page: number | null): Observable<RecipesPage>;

  abstract getFavoriteRecipes(pageSize: number, page: number | null): Observable<RecipesPage>;

  abstract getRecipeOfTheDay(): Observable<RecipePreview>;
}
