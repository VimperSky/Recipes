import {Injectable} from "@angular/core";
import {ActivityService} from "../abstract/activity.service";
import {Observable} from "rxjs";
import {environment} from "../../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {UserActivityDto} from "../../../dto/activity/user-activity-dto";
import {MyRecipesActivityDto} from "../../../dto/activity/my-recipes-activity-dto";

const basePath: string = "/api/activity"

@Injectable()
export class ApiActivityService extends ActivityService {
  constructor(private http: HttpClient) {
    super();
  }

  setLike(recipeId: number): Observable<void> {
    return this.http.put<void>(environment.backendUrl + basePath + `/like?recipeId=${recipeId}`, null);
  }

  deleteLike(recipeId: number): Observable<void> {
    return this.http.delete<void>(environment.backendUrl + basePath + `/like?recipeId=${recipeId}`);
  }

  setStar(recipeId: number): Observable<void> {
    return this.http.put<void>(environment.backendUrl + basePath + `/star?recipeId=${recipeId}`, null);
  }

  deleteStar(recipeId: number): Observable<void> {
    return this.http.delete<void>(environment.backendUrl + basePath + `/star?recipeId=${recipeId}`);
  }

  getUserActivity(dto: MyRecipesActivityDto): Observable<UserActivityDto> {
    return this.http.post<UserActivityDto>(environment.backendUrl + basePath + '/myRecipesActivity', dto);
  }
}
