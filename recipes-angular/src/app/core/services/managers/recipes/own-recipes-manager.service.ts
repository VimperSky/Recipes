import {Injectable} from "@angular/core";
import {RecipesManagerService} from "./recipes-manager.service";
import {Observable} from "rxjs";
import {RecipesPage} from "../../../dto/recipe/recipes-page";

@Injectable()
export class OwnRecipesManagerService extends RecipesManagerService {
  getRecipes(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    return this.recipesService.getMyRecipes(pageSize, page);
  }

}
