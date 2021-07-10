import { Injectable } from '@angular/core';
import {RecipePreview} from "../models/recipe-preview";

@Injectable()
export abstract class RecipesService {

  abstract search(searchString: string): void;

  abstract loadMore(): void;

  abstract get recipes(): RecipePreview[];

  abstract get hasMore(): boolean;
}
