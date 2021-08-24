import {Component, OnInit} from '@angular/core';
import {BaseRecipesManagerService} from "../../core/services/managers/recipes/base-recipes-manager.service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {
  constructor(public recipesManager: BaseRecipesManagerService) {}

  ngOnInit(): void {
    this.recipesManager.loadInitial();
  }

  loadMore() {
    this.recipesManager.loadMore()
  }
}
