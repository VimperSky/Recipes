import {RecipeBase} from "./recipe-base";
import {AuthorDto} from "../user/author-dto";

export interface RecipePreview extends RecipeBase {
  imagePath: string | undefined;
  id: number;
  author: AuthorDto;
}
