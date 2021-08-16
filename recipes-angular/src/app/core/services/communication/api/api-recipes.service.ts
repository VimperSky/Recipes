import {Injectable} from '@angular/core';
import {RecipesService} from "../abstract/recipes.service";
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {Observable} from "rxjs";

const basePath: string = "/api/recipes"

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

    return this.http.get<RecipesPage>(environment.backendUrl + basePath + "/list", {params: params});
  }

}
