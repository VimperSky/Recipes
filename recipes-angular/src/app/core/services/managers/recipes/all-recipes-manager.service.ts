import {Injectable} from '@angular/core';
import {environment} from "../../../../../environments/environment";
import {RecipesManager} from "./recipes-manager.service";
import {Observable} from "rxjs";
import {RecipesPage} from "../../../dto/recipe/recipes-page";

@Injectable()
export class AllRecipesManagerService extends RecipesManager {
  private searchString: string | null = null;

  public search(searchString: string | null) {
    if (this.isPendingAction)
      return;
    if (searchString == "")
      searchString = null;

    this.isPendingAction = true;
    this.getRecipes(environment.pageSize, 1, searchString).subscribe(result => {
      this.updateRecipeList(result, true);
      this.searchString = searchString;
    });
  }

  getRecipes(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    return this.recipesService.getRecipeList(pageSize, page, searchString);
  }
}
