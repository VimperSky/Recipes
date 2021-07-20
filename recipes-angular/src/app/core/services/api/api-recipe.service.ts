import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RecipeService} from "../recipe.service";
import {RecipeDetail} from "../../models/recipe-detail";
import {environment} from "../../../../environments/environment";
import {Observable} from "rxjs";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

@Injectable()
export class ApiRecipeService extends RecipeService {

  constructor(private http: HttpClient) {
    super();
  }

  detail(id: number): Observable<RecipeDetail> {
    return this.http.get<RecipeDetail>(environment.backendUrl + `api/recipe/detail?id=${id}`, httpOptions);
  }

}
