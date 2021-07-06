import { Injectable } from '@angular/core';
import {Recipe} from "../models/recipe";

export const recipes: Recipe[] = [
  {
    imagePath: "../../../assets/images/r1.png",
    header: "Клубичная Панна-Котта",
    description: "Десерт, который невероятно легко и быстро готовится. " +
      "Советую подавать его порционно в красивых бокалах, украсив взбитыми сливками, свежими ягодами и мятой.",
    cookingTime: 35,
    portions: 5
  },
  {
    imagePath: "../../../assets/images/r2.png",
    header: "Мясные фрикадельки",
    description: "Мясные фрикадельки в томатном соусе - несложное и вкусное блюдо, которым можно порадовать своих близких. ",
    cookingTime: 90,
    portions: 4
  },
  {
    imagePath: "../../../assets/images/r3.png",
    header: "Панкейки",
    description: "Панкейки: меньше, чем блины, но больше, чем оладьи. Основное отличие — в тесте, " +
      "оно должно быть воздушным, чтобы панкейки не растекались по сковородке...",
    cookingTime: 40,
    portions: 3
  },
  {
    imagePath: "../../../assets/images/r4.png",
    header: "Полезное мороженое без сахара",
    description: "Йогуртовое мороженое сочетает в себе нежный вкус и низкую калорийность, " +
      "что будет особенно актуально для сладкоежек, соблюдающих диету.",
    cookingTime: 35,
    portions: 2
  },
  {
    imagePath: "../../../assets/images/r5.jpg",
    header: "БИСКВИТ КЛАССИЧЕСКИЙ",
    description: "Сегодня хочу показать, как приготовить идеально пышный бисквит для торта или пирожных." +
      " Очень простой рецепт без использования разрыхлителя и соды. Только мука, яйца, сахар и небольшая щепотка соли. Именно такие ингредиенты используются для классического пышного бисквита",
    cookingTime: 400,
    portions: 6
  }
]


@Injectable({
  providedIn: 'root'
})
export class RecipesManagerService {
  public recipes: Recipe[] = [];

  private loadedCount = 1;

  private get loadCount(): number {
    return 4 * this.loadedCount;
  }

  constructor() {
    this.recipes = recipes.slice(0, this.loadCount);
  }

  search(searchString: string) {
    this.recipes = recipes.filter(x => x.header.toLowerCase().includes(searchString.toLowerCase())).slice(0, this.loadCount)
  }

  loadMore() {
    this.loadedCount++;
    this.recipes = recipes.slice(0, this.loadCount)
  }

  public get hasMore(): boolean {
    return this.loadedCount <= 1;
  }
}
