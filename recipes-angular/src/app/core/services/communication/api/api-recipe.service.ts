import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {RecipeService} from "../abstract/recipe.service";
import {RecipeDetail} from "../../../dto/recipe/recipe-detail";
import {environment} from "../../../../../environments/environment";
import {Observable} from "rxjs";
import {RecipeCreate} from "../../../dto/recipe/recipe-create";
import {RecipeEdit} from "../../../dto/recipe/recipe-edit";

const basePath: string = "/api/recipe"

@Injectable()
export class ApiRecipeService extends RecipeService {
  constructor(private http: HttpClient) {
    super();
  }

  detail(id: number): Observable<RecipeDetail> {
    return this.http.get<RecipeDetail>(environment.backendUrl + basePath + `/detail?id=${id}`);
  }

  create(recipeCreate: RecipeCreate): Observable<number> {
    return this.http.post<number>(environment.backendUrl + basePath + `/create`, recipeCreate);
  }

  delete(id: number): Observable<void> {
    let params = new HttpParams();
    params = params.append("id", id)
    return this.http.delete<void>(environment.backendUrl + basePath + `/delete`,
      {params: params})
  }

  edit(recipeEdit: RecipeEdit): Observable<void> {
    return this.http.patch<void>(environment.backendUrl + basePath + `/edit`, recipeEdit)
  }

  uploadImage(recipeId: number, file: File): Observable<void> {
    let formData = new FormData();
    formData.set("recipeId", recipeId.toString());
    formData.set("file", file, file.name);

    return this.http.put<void>(environment.backendUrl + basePath + `/uploadImage`, formData)
  }
}
