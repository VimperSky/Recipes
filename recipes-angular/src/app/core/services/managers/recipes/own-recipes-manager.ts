import {Injectable} from "@angular/core";
import {RecipesManager} from "./recipes-manager.service";
import {Observable} from "rxjs";
import {RecipesPage} from "../../../dto/recipe/recipes-page";

@Injectable()
export class OwnRecipesManager extends RecipesManager {
  getRecipes(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    return this.recipesService.getMyRecipes(pageSize, page);
  }

}
