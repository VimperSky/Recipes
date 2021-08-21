export interface RecipeBase {
  name: string;
  description: string;
  cookingTimeMin: number | undefined;
  portions: number | undefined;
  tags: string[];
}
