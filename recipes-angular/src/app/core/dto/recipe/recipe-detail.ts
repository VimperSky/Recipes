import {RecipeBase} from "./recipe-base";
import {Ingredient} from "./ingredient";

export interface RecipeDetail extends RecipeBase {
  id: number;
  imagePath: string;
  ingredients: Ingredient[];
  steps: string[];
}
