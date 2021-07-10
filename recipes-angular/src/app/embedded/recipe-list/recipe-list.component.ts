import {Component, OnInit} from '@angular/core';
import {RecipesService} from "../../core/services/recipes.service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {

  constructor(public recipesProvider: RecipesService) { }

  ngOnInit(): void {
  }

  loadMore() {
    this.recipesProvider.loadMore();
  }

  public get hasMore(): boolean {
    return this.recipesProvider.hasMore;
  }
}
