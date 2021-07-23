import { Injectable } from '@angular/core';
import {RecipeDetail} from "../../models/recipe-detail";
import {Observable} from "rxjs";

@Injectable()
export abstract class RecipeService {
  abstract detail(id: number): Observable<RecipeDetail>;
}
