import { Injectable } from '@angular/core';
import {Recipe} from "../models/recipe";

@Injectable()
export abstract class RecipesManagerService {
  public recipes: Recipe[] = [];

  abstract search(searchString: string): void;

  abstract loadMore(): void;

  abstract get hasMore(): boolean;
}
