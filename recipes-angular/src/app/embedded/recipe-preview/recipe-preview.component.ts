import {Component, Input, OnInit} from '@angular/core';
import {RecipePreview} from "../../core/models/recipe-preview";
import {environment} from "../../../environments/environment";

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

  getImagePath(recipe: RecipePreview): string {
    return environment.apiUrl + "/" + recipe.imagePath;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
