import {RecipePreview} from "./recipe-preview";

export interface RecipePage {
  recipes: RecipePreview[];
  hasMore: boolean;
}
