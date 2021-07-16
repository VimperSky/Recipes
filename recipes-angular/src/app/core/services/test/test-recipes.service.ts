import { Injectable } from '@angular/core';
import {RecipesService} from "../recipes.service";
import {RecipePreview} from "../../models/recipe-preview";
import {RecipePage} from "../../models/recipe-page";
import {CommandJsonPathException} from "@angular/cli/utilities/json-schema";
import {Observable, of, throwError} from "rxjs";

const testImagesPath =  "../../../assets/images/test/";

export const recipes: RecipePreview[] = [
  {
    id: 1,
    imagePath: testImagesPath + "r1.png",
    name: "Клубичная Панна-Котта",
    description: "Десерт, который невероятно легко и быстро готовится. " +
      "Советую подавать его порционно в красивых бокалах, украсив взбитыми сливками, свежими ягодами и мятой.",
    cookingTime: 35,
    portions: 5
  },
  {
    id: 2,
    imagePath: testImagesPath + "r2.png",
    name: "Мясные фрикадельки",
    description: "Мясные фрикадельки в томатном соусе - несложное и вкусное блюдо, которым можно порадовать своих близких. ",
    cookingTime: 90,
    portions: 4
  },
  {
    id: 3,
    imagePath: testImagesPath + "r3.png",
    name: "Панкейки",
    description: "Панкейки: меньше, чем блины, но больше, чем оладьи. Основное отличие — в тесте, " +
      "оно должно быть воздушным, чтобы панкейки не растекались по сковородке...",
    cookingTime: 40,
    portions: 3
  },
  {
    id: 4,
    imagePath: testImagesPath + "r4.png",
    name: "Полезное мороженое без сахара",
    description: "Йогуртовое мороженое сочетает в себе нежный вкус и низкую калорийность, " +
      "что будет особенно актуально для сладкоежек, соблюдающих диету.",
    cookingTime: 35,
    portions: 2
  },
  {
    id: 5,
    imagePath: testImagesPath + "r5.jpg",
    name: "БИСКВИТ КЛАССИЧЕСКИЙ",
    description: "Сегодня хочу показать, как приготовить идеально пышный бисквит для торта или пирожных." +
      " Очень простой рецепт без использования разрыхлителя и соды. Только мука, яйца, сахар и небольшая щепотка соли. Именно такие ингредиенты используются для классического пышного бисквита",
    cookingTime: 400,
    portions: 6
  }
]

const pageSize: number = 4;

@Injectable()
export class TestRecipesService extends RecipesService{

  constructor() {
    super();
  }

  getRecipeList(page: number | null, searchString: string | null): Observable<RecipePage> {
    if (page == null)
      page = 1;

    let result = recipes;
    if (searchString != null) {
      result = result.filter(x => x.name.toLowerCase().includes(searchString.toLowerCase()));
    }

    let skipElements = (page - 1) * pageSize;
    if (skipElements > result.length) {
      throwError("This page doesn't exist");
    }
    result = result.slice(skipElements);

    let hasMore = false;
    if (result.length > pageSize) {
      result = result.slice(0, pageSize);
      hasMore = true;
    }


    let recipePage: RecipePage = {recipes: result, hasMore: hasMore};

    return of(recipePage);
  }

}
