import {Component, Input, OnInit} from '@angular/core';
import {RecipePreview} from "../../core/dto/recipe/recipe-preview";
import {environment} from "../../../environments/environment";

const defaultImagePath: string = "../../../assets/images/default_recipe.jpg"

@Component({
  selector: 'app-recipe-preview',
  templateUrl: './recipe-preview.component.html',
  styleUrls: ['./recipe-preview.component.scss']
})
export class RecipePreviewComponent {

  @Input()
  public recipe!: RecipePreview;

  @Input()
  public isClickable: boolean = true;

  public get login(): string {
    if (this.recipe.author != null)
      return "@" + this.recipe.author.login;

    return "@admin";
  }

  public getImagePath(): string {
    if (this.recipe.imagePath) {
      return RecipePreviewComponent.isLocalImagePath(this.recipe.imagePath)
        ? this.recipe.imagePath
        : environment.backendUrl + "/" + this.recipe.imagePath;
    }
    return defaultImagePath;
  }

  private static isLocalImagePath(imagePath: string): boolean {
    return imagePath.startsWith("..");
  }
}
