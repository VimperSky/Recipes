import { Injectable } from '@angular/core';
import {RecipeDetail} from "../models/recipe-detail";

@Injectable({
  providedIn: 'root'
})
export abstract class RecipeService {
  abstract detail(id: number): RecipeDetail;
}
