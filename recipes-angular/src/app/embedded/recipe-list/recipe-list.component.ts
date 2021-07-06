import { Component, OnInit } from '@angular/core';
import {Recipe} from "../../core/models/recipe";

const recipes: Recipe[] = [
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
  }
]

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.scss']
})
export class RecipeListComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }


  public recipes = recipes;
}
