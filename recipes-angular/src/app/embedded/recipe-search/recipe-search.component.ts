import {Component, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {RecipesManagerService} from "../../core/services/recipes-manager.service";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  searchString = new FormControl('', []);

  constructor(public recipesProvider: RecipesManagerService) { }

  ngOnInit(): void {
  }

  search() {
    this.recipesProvider.search(this.searchString.value);
  }

}
