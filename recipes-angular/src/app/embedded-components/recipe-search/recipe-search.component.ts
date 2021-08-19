import {Component, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {BaseRecipesManagerService} from "../../core/services/managers/recipes/base-recipes-manager.service";
import {AllRecipesManagerService} from "../../core/services/managers/recipes/all-recipes-manager.service";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  searchString = new FormControl('', []);
  public recipesManager: AllRecipesManagerService;

  constructor(recipesManager: BaseRecipesManagerService) {
    this.recipesManager = recipesManager as AllRecipesManagerService
  }

  ngOnInit(): void {
  }

  search() {
    this.recipesManager.search(this.searchString.value);
  }

}
