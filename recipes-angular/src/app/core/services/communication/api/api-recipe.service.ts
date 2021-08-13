import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {RecipeService} from "../abstract/recipe.service";
import {RecipeDetail} from "../../../dto/recipe/recipe-detail";
import {environment} from "../../../../../environments/environment";
import {Observable, of} from "rxjs";
import {RecipeCreate} from "../../../dto/recipe/recipe-create";
import {RecipeEdit} from "../../../dto/recipe/recipe-edit";
import {AuthTokenManagerService} from "../../managers/auth-token-manager.service";

const basePath: string = "/api/recipe"

@Injectable()
export class ApiRecipeService extends RecipeService {
  get optionsWithAuth() {
    let newOptions = {headers: new HttpHeaders()};
    newOptions.headers = newOptions.headers.set('Authorization', 'Bearer ' + this.tokenManager.tokenValue);
    return newOptions;
  }

  constructor(private http: HttpClient, private tokenManager: AuthTokenManagerService) {
    super();
  }

  detail(id: number): Observable<RecipeDetail> {
    return this.http.get<RecipeDetail>(environment.backendUrl + basePath + `/detail?id=${id}`);
  }

  create(recipeCreate: RecipeCreate): Observable<number> {
    return this.http.post<number>(environment.backendUrl + basePath + `/create`, recipeCreate, this.optionsWithAuth);
  }

  delete(id: number): Observable<void> {
    let params = new HttpParams();
    params = params.append("id", id)
    return this.http.delete<void>(environment.backendUrl + basePath + `/delete`, {...this.optionsWithAuth, params: params})
  }

  edit(recipeEdit: RecipeEdit): Observable<void> {
    return this.http.patch<void>(environment.backendUrl + basePath + `/edit`, recipeEdit, this.optionsWithAuth)
  }

  uploadImage(recipeId: number, file: File): Observable<void> {
    let formData = new FormData();
    formData.set("recipeId", recipeId.toString());
    formData.set("file", file, file.name);

    return this.http.put<void>(environment.backendUrl + basePath + `/uploadImage`, formData, this.optionsWithAuth)
  }
}
