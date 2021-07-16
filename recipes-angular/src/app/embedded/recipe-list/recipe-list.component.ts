import {Component, OnInit} from '@angular/core';
import {RecipesManagerService} from "../../core/services/recipes-manager.service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {

  constructor(public recipesManager: RecipesManagerService) { }

  ngOnInit(): void {
    this.recipesManager.loadInitial();
  }

  loadMore() {
    this.recipesManager.loadMore()
  }
}
