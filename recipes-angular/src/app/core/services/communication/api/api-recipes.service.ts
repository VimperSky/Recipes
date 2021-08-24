import {Injectable} from '@angular/core';
import {RecipesService} from "../abstract/recipes.service";
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {RecipesPage} from "../../../dto/recipe/recipes-page";
import {Observable} from "rxjs";
import {HttpParamsBuilderService} from "../../tools/http-params-builder.service";
import {RecipePreview} from "../../../dto/recipe/recipe-preview";

const basePath: string = "/api/recipes"

@Injectable()
export class ApiRecipesService extends RecipesService {

  constructor(private http: HttpClient, private paramsBuilder: HttpParamsBuilderService) {
    super();
  }

  public getRecipeList(pageSize: number, page: number | null, searchString: string | null): Observable<RecipesPage> {
    return this.processListRequest("/list", pageSize, page, searchString);
  }

  public getMyRecipes(pageSize: number, page: number | null): Observable<RecipesPage> {
    return this.processListRequest("/myList", pageSize, page);
  }

  public getFavoriteRecipes(pageSize: number, page: number | null): Observable<RecipesPage> {
    return this.processListRequest("/favoriteList", pageSize, page);
  }

  public getRecipeOfTheDay(): Observable<RecipePreview> {
    return this.http.get<RecipePreview>(environment.backendUrl + basePath + "/recipeOfDay");
  }

  private processListRequest(path: string, pageSize: number, page: number | null, searchString: string | null = null) {
    let params = new HttpParams();

    params = params.append("pageSize", pageSize)
    if (page) params = params.append("page", page);
    if (searchString) params = params.append('searchString', searchString);

    return this.http.get<RecipesPage>(environment.backendUrl + basePath + path,
      {...this.paramsBuilder.authOptions, params: params});
  }

}
