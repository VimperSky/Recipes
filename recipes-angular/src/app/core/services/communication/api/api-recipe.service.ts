import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RecipeService} from "../abstract/recipe.service";
import {RecipeDetail} from "../../../dto/recipe/recipe-detail";
import {environment} from "../../../../../environments/environment";
import {Observable} from "rxjs";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': "*"
  })
};

const basePath: string = "/api/recipe"

@Injectable()
export class ApiRecipeService extends RecipeService {
  constructor(private http: HttpClient) {
    super();
  }

  detail(id: number): Observable<RecipeDetail> {
    return this.http.get<RecipeDetail>(environment.backendUrl + basePath + `/detail?id=${id}`, httpOptions);
  }

}