import {Component, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {RecipesService} from "../../core/services/recipes.service";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  searchString = new FormControl('', []);

  constructor(public recipesProvider: RecipesService) { }

  ngOnInit(): void {
  }

  search() {
    this.recipesProvider.search(this.searchString.value);
  }

}
