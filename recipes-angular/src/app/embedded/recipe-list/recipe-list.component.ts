import {Component, OnInit} from '@angular/core';
import {RecipesManagerService} from "../../core/services/recipes-manager.service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {

  constructor(public recipesProvider: RecipesManagerService) { }

  ngOnInit(): void {
  }

  loadMore() {
    this.recipesProvider.loadMore();
  }

  public get hasMore(): boolean {
    return this.recipesProvider.hasMore;
  }
}
