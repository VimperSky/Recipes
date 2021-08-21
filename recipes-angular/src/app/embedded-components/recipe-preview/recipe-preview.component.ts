import {Component, Input, OnInit} from '@angular/core';
import {RecipePreview} from "../../core/dto/recipe/recipe-preview";
import {environment} from "../../../environments/environment";

const defaultImagePath: string = "../../../assets/images/default_recipe.jpg"

@Component({
  selector: 'app-recipe-preview',
  templateUrl: './recipe-preview.component.html',
  styleUrls: ['./recipe-preview.component.scss']
})
export class RecipePreviewComponent implements OnInit {

  @Input()
  recipe!: RecipePreview;

  @Input()
  isClickable: boolean = true;

  get login(): string {
    if (this.recipe.author != null)
      return "@" + this.recipe.author.login;

    return "@admin";
  }

  private static isLocalImagePath(imagePath: string): boolean {
    return imagePath.startsWith("..");
  }

  getImagePath(recipe: RecipePreview): string {
    if (recipe.imagePath) {
      return RecipePreviewComponent.isLocalImagePath(recipe.imagePath)
        ? recipe.imagePath
        : environment.backendUrl + "/" + recipe.imagePath;
    }
    return defaultImagePath;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
