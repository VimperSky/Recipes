import {Injectable} from "@angular/core";
import {ActivityService} from "../abstract/activity.service";
import {Observable} from "rxjs";
import {environment} from "../../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {HttpParamsBuilderService} from "../../tools/http-params-builder.service";
import {UserActivity} from "../../../dto/user/user-activity";

const basePath: string = "/api/activity"

@Injectable()
export class ApiActivityService extends ActivityService {
  constructor(private http: HttpClient, private paramsBuilder: HttpParamsBuilderService) {
    super();
  }

  setLike(recipeId: number): Observable<void> {
    return this.http.put<void>(environment.backendUrl + basePath + `/like?recipeId=${recipeId}`, null,
      this.paramsBuilder.authOptions);
  }

  deleteLike(recipeId: number): Observable<void> {
    return this.http.delete<void>(environment.backendUrl + basePath + `/like?recipeId=${recipeId}`,
      this.paramsBuilder.authOptions);
  }

  setStar(recipeId: number): Observable<void> {
    return this.http.put<void>(environment.backendUrl + basePath + `/star?recipeId=${recipeId}`, null,
      this.paramsBuilder.authOptions);
  }

  deleteStar(recipeId: number): Observable<void> {
    return this.http.delete<void>(environment.backendUrl + basePath + `/star?recipeId=${recipeId}`,
      this.paramsBuilder.authOptions);
  }

  getUserActivity(): Observable<UserActivity> {
    return this.http.get<UserActivity>(environment.backendUrl + basePath + '/userActivity',
      this.paramsBuilder.authOptions);
  }
}
