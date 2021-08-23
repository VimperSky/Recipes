import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {UserActivity} from "../../../dto/user/user-activity";

@Injectable()
export abstract class ActivityService {
  abstract setLike(recipeId: number): Observable<void>;

  abstract deleteLike(recipeId: number): Observable<void>;


  abstract setStar(recipeId: number): Observable<void>;

  abstract deleteStar(recipeId: number): Observable<void>;

  abstract getUserActivity(): Observable<UserActivity>;
}
