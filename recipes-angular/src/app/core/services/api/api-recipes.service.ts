import { Injectable } from '@angular/core';
import {RecipesService} from "../recipes.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RecipePreview} from "../../models/recipe-preview";
import {environment} from "../../../../environments/environment";
import {RecipePage} from "../../models/recipe-page";
import {Observable} from "rxjs";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

@Injectable()
export class ApiRecipesService extends RecipesService {


  constructor(private http: HttpClient) {
    super();
  }


  public getRecipeList(page: number | null, searchString: string | null): Observable<RecipePage> {
    return this.http.get<RecipePage>(environment.apiUrl + "/recipes/list", httpOptions);
  }

}
