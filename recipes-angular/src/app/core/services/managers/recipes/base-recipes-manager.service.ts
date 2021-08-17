import {Injectable} from '@angular/core';
import {RecipePreview} from "../../../dto/recipe/recipe-preview";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {RecipesService} from "../../communication/abstract/recipes.service";
import {environment} from "../../../../../environments/environment";

@Injectable()
export abstract class BaseRecipesManagerService {
  abstract loadInitial(): void;
  abstract loadMore(): void;
  abstract get hasMore(): boolean;
  abstract get recipeList(): RecipePreview[];
}
