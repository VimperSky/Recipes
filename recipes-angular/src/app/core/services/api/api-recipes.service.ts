import { Injectable } from '@angular/core';
import {RecipesService} from "../abstract/recipes.service";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {RecipesPage} from "../../dto/recipes-page";
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


  public getRecipeList(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    let params = new HttpParams();

    params = params.append("pageSize", pageSize)
    if (page) params = params.append("page", page);
    if (searchString) params = params.append('searchString', searchString);

    return this.http.get<RecipesPage>(environment.backendUrl + "api/recipes/list", {...httpOptions, params: params});
  }

}
