import {Injectable} from '@angular/core';
import {RecipePreview} from "../../../dto/recipe/recipe-preview";

@Injectable()
export abstract class BaseRecipesManagerService {
  abstract loadInitial(): void;
  abstract loadMore(): void;
  abstract get hasMore(): boolean;
  abstract get recipeList(): RecipePreview[];
}
