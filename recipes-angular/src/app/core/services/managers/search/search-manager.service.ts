import {Injectable} from '@angular/core';
import {AllRecipesManagerService} from "../recipes/all-recipes-manager.service";
import {RecipesManagerService} from "../recipes/recipes-manager.service";
import {FormControl} from "@angular/forms";
import {BaseSearchManagerService} from "./base-search-manager.service";
import {ActivatedRoute, Router} from "@angular/router";

@Injectable()
export class SearchManagerService extends BaseSearchManagerService {
  public searchString = new FormControl('', []);
  private recipesManager: AllRecipesManagerService;

  constructor(recipesManager: RecipesManagerService,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
    super();
    this.recipesManager = recipesManager as AllRecipesManagerService;
  }

  public setString(value: string) {
    this.searchString.setValue(value);
  }

  public search() {
    this.recipesManager.search(this.searchString.value);
    this.router.navigate(['/recipes'], {
      relativeTo: this.activatedRoute,
      queryParams: {searchString: this.searchString.value == "" ? null : this.searchString.value},
      queryParamsHandling: "merge"
    })

  }
}

