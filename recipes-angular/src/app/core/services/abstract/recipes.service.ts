import { Injectable } from '@angular/core';
import {RecipePage} from "../../models/recipe-page";
import {Observable} from "rxjs";

@Injectable()
export abstract class RecipesService {
  abstract getRecipeList(pageSize: number, page: number | null, searchString: string | null): Observable<RecipePage>;
}
