import {RecipeBase} from "./recipe-base";

export interface RecipePreview extends RecipeBase {
  imagePath: string | undefined;
  id: number;
}
