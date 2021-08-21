import {Injectable} from '@angular/core';
import {AllRecipesManagerService} from "../recipes/all-recipes-manager.service";
import {BaseRecipesManagerService} from "../recipes/base-recipes-manager.service";
import {FormControl} from "@angular/forms";
import {BaseSearchManagerService} from "./base-search-manager.service";
import {Router} from "@angular/router";
import {state} from "@angular/animations";

@Injectable()
export class RedirectSearchManagerService extends BaseSearchManagerService {
  constructor(private router: Router) {
    super();
  }

  public searchString = new FormControl('', []);

  public setString(value: string) {
    this.searchString.setValue(value);
  }

  public search() {
    this.router.navigate(['/recipes'], {queryParams: {searchString: this.searchString.value}})
  }
}

