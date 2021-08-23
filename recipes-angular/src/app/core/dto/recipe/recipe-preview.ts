import {RecipeBase} from "./recipe-base";
import {AuthorDto} from "../user/author-dto";

export interface RecipePreview extends RecipeBase {
  imagePath: string | undefined;
  id: number;
  author: AuthorDto;
  starsCount: number;
  likesCount: number;

  isLiked: boolean;
  isStarred: boolean;
}
