import {Component, OnInit} from '@angular/core';
import {RecipePreview} from "../../core/dto/recipe/recipe-preview";
import {environment} from "../../../environments/environment";
import {RecipesService} from "../../core/services/communication/abstract/recipes.service";

@Component({
  selector: 'app-recipe-of-the-day',
  templateUrl: './recipe-of-the-day.component.html',
  styleUrls: ['./recipe-of-the-day.component.scss']
})
export class RecipeOfTheDayComponent implements OnInit {
  public recipe: RecipePreview | undefined;

  public constructor(private recipesService: RecipesService) {
  }

  public get login(): string {
    if (!this.recipe)
      throw "recipe is null";

    if (this.recipe.author != null)
      return "@" + this.recipe.author.login;

    return "@admin";
  }

  public getImagePath(): string {
    if (!this.recipe)
      throw "recipe is null";
    return environment.backendUrl + "/" + this.recipe.imagePath;
  }

  public ngOnInit() {
    this.recipesService.getRecipeOfTheDay().subscribe((recipe: RecipePreview) => {
      this.recipe = recipe;
    })
  }
}
