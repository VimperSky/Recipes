import {Ingredient} from "./ingredient";
import {RecipePreview} from "./recipe-preview";

export interface RecipeDetail extends RecipePreview {
  ingredients: Ingredient[];
  steps: string[];
}
