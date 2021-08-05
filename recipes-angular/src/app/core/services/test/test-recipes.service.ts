import {Injectable} from '@angular/core';
import {RecipesService} from "../abstract/recipes.service";
import {RecipePreview} from "../../dto/recipe/recipe-preview";
import {RecipesPage} from "../../dto/recipe/recipes-page";
import {Observable, of, throwError} from "rxjs";

const testImagesPath =  "../../../assets/images/test/";

export const recipes: RecipePreview[] = [
  {
    id: 1,
    imagePath: testImagesPath + "r1.png",
    name: "Клубичная Панна-Котта",
    description: "Десерт, который невероятно легко и быстро готовится. " +
      "Советую подавать его порционно в красивых бокалах, украсив взбитыми сливками, свежими ягодами и мятой.",
    cookingTimeMin: 35,
    portions: 5
  },
  {
    id: 2,
    imagePath: testImagesPath + "r2.png",
    name: "Мясные фрикадельки",
    description: "Мясные фрикадельки в томатном соусе - несложное и вкусное блюдо, которым можно порадовать своих близких. ",
    cookingTimeMin: 90,
    portions: 4
  },
  {
    id: 3,
    imagePath: testImagesPath + "r3.png",
    name: "Панкейки",
    description: "Панкейки: меньше, чем блины, но больше, чем оладьи. Основное отличие — в тесте, " +
      "оно должно быть воздушным, чтобы панкейки не растекались по сковородке...",
    cookingTimeMin: 40,
    portions: 3
  },
  {
    id: 4,
    imagePath: testImagesPath + "r4.png",
    name: "Полезное мороженое без сахара",
    description: "Йогуртовое мороженое сочетает в себе нежный вкус и низкую калорийность, " +
      "что будет особенно актуально для сладкоежек, соблюдающих диету.",
    cookingTimeMin: 35,
    portions: 2
  },
  {
    id: 5,
    imagePath: testImagesPath + "r5.jpg",
    name: "БИСКВИТ КЛАССИЧЕСКИЙ",
    description: "Сегодня хочу показать, как приготовить идеально пышный бисквит для торта или пирожных." +
      " Очень простой рецепт без использования разрыхлителя и соды. Только мука, яйца, сахар и небольшая щепотка соли. Именно такие ингредиенты используются для классического пышного бисквита",
    cookingTimeMin: 400,
    portions: 6
  }
]

@Injectable()
export class TestRecipesService extends RecipesService{

  constructor() {
    super();
  }

  getRecipeList(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    if (pageSize <= 0)
      throwError('pageSize should be 1 or more')

    if (page == null || page <= 0)
      page = 1;

    let result = recipes;
    if (searchString != null) {
      result = result.filter(x => x.name.toLowerCase().includes(searchString.toLowerCase()));
    }

    let pageCount = Math.ceil(result.length / pageSize);
    if (page > pageCount) {
      throwError("This page doesn't exist");
    }

    result = result.slice((page - 1) * pageSize).slice(0, pageSize);

    let recipePage: RecipesPage = {recipes: result, pageCount: pageCount};

    return of(recipePage);
  }

}
