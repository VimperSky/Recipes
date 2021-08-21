import {Injectable} from '@angular/core';
import {AllRecipesManagerService} from "./all-recipes-manager.service";
import {BaseRecipesManagerService} from "./base-recipes-manager.service";
import {FormControl} from "@angular/forms";

@Injectable()
export class SearchManagerService {
  private recipesManager: AllRecipesManagerService;

  constructor(recipesManager: BaseRecipesManagerService) {
    this.recipesManager = recipesManager as AllRecipesManagerService;
  }

  public searchString = new FormControl('', []);

  public setString(value: string) {
    this.searchString.setValue(value);
  }

  public search() {
    this.recipesManager.search(this.searchString.value);
  }
}

