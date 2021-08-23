import {Component, OnInit} from '@angular/core';
import {RecipesManager} from "../../core/services/managers/recipes/recipes-manager.service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {
  constructor(public recipesManager: RecipesManager) {}

  ngOnInit(): void {
    this.recipesManager.loadInitial();
  }

  loadMore() {
    this.recipesManager.loadMore()
  }
}
