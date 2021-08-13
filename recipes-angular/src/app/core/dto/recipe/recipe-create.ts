import {RecipeBase} from "./recipe-base";
import {Ingredient} from "./ingredient";

export interface RecipeCreate extends RecipeBase {
  ingredients: Ingredient[];
  steps: string[];
}
