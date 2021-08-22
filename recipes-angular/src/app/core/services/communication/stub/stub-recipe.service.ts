import {Injectable} from '@angular/core';
import {RecipeService} from "../abstract/recipe.service";
import {RecipeDetail} from "../../../dto/recipe/recipe-detail";
import {Observable, of} from "rxjs";
import {RecipeCreate} from "../../../dto/recipe/recipe-create";
import {RecipeEdit} from "../../../dto/recipe/recipe-edit";

const testImagesPath = "../../../assets/images/test/";
const recipeDetail: RecipeDetail = {
  id: 1,
  authorId: 1,
  imagePath: testImagesPath + "r1.png",
  name: "Клубичная Панна-Котта",
  description: "Десерт, который невероятно легко и быстро готовится. " +
    "Советую подавать его порционно в красивых бокалах, украсив взбитыми сливками, свежими ягодами и мятой.",
  cookingTimeMin: 35,
  portions: 5,
  steps: ['Приготовим панна котту: Зальем желатин молоком и поставим на 30 минут для набухания. ' +
  'В сливки добавим сахар и ванильный сахар. Доводим до кипения (не кипятим!).',

    'Добавим в сливки набухший в молоке желатин. Перемешаем до полного растворения. Огонь отключаем. ' +
    'Охладим до комнатной температуры.',

    'Разольем охлажденные сливки по креманкам и поставим в холодильник до полного застывания. Это около 3-5 часов.',

    'Приготовим клубничное желе: Клубнику помоем, очистим от плодоножек. Добавим сахар.' +
    ' Взбиваем клубнику с помощью блендера в пюре.',

    'Добавим в миску с желатином 2ст.ложки холодной воды и сок лимона. Перемешаем и поставим на 30 минут для набухания. ' +
    'Доведем клубничное пюре до кипения. Добавим набухший желатин, перемешаем до полного растворения. ' +
    'Огонь отключаем. Охладим до комнатной температуры.',

    'Сверху на застывшие сливки добавим охлажденное клубничное желе. Поставим в холодильник ' +
    'до полного застывания клубничного желе. Готовую панна коту подаем с фруктами.'
  ],
  ingredients: [
    {
      header: 'Для панна котты',
      value: 'Сливки-20-30% - 500мл.\n' +
        'Молоко - 100мл.\n' +
        'Желатин - 2ч.л.\n' +
        'Сахар - 3ст.л.\n' +
        'Ванильный сахар - 2 ч.л.'
    },
    {
      header: 'Для клубничного желе',
      value: 'Сливки-20-30% - 500мл.\n' +
        'Молоко - 100мл.\n' +
        'Желатин - 2ч.л.\n' +
        'Сахар - 3ст.л.\n' +
        'Ванильный сахар - 2 ч.л.'
    }
  ],
  tags: ["десерты", "клубника", "сливки"]
};

@Injectable({
  providedIn: 'root'
})
export class StubRecipeService implements RecipeService {
  constructor() {
  }

  detail(id: number): Observable<RecipeDetail> {
    return of(recipeDetail);
  }

  create(recipeCreate: RecipeCreate): Observable<number> {
    throw "Not Implemented";
  }

  delete(id: number): Observable<void> {
    throw "Not Implemented";
  }

  edit(recipeEdit: RecipeEdit): Observable<void> {
    throw "Not Implemented";
  }

  uploadImage(recipeId: number, file: File): Observable<void> {
    throw "Not Implemented";
  }
}
