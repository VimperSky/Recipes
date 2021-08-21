import {Injectable} from '@angular/core';
import {AllRecipesManagerService} from "../recipes/all-recipes-manager.service";
import {BaseRecipesManagerService} from "../recipes/base-recipes-manager.service";
import {FormControl} from "@angular/forms";
import {BaseSearchManagerService} from "./base-search-manager.service";
import {ActivatedRoute, Router} from "@angular/router";

@Injectable()
export class SearchManagerService extends BaseSearchManagerService {
  private recipesManager: AllRecipesManagerService;

  constructor(recipesManager: BaseRecipesManagerService,
              private activatedRoute: ActivatedRoute,
              private router: Router) {
    super();
    this.recipesManager = recipesManager as AllRecipesManagerService;
  }

  public searchString = new FormControl('', []);

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

