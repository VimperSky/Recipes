import {Injectable} from '@angular/core';
import {RecipeDetail} from "../../../dto/recipe/recipe-detail";
import {Observable} from "rxjs";
import {RecipeCreate} from "../../../dto/recipe/recipe-create";
import {RecipeEdit} from "../../../dto/recipe/recipe-edit";

@Injectable()
export abstract class RecipeService {
  abstract detail(id: number): Observable<RecipeDetail>;

  abstract create(recipeCreate: RecipeCreate): Observable<number>;

  abstract edit(recipeEdit: RecipeEdit): Observable<void>;

  abstract delete(id: number): Observable<void>;

  abstract uploadImage(recipeId: number, file: File): Observable<void>;
}
