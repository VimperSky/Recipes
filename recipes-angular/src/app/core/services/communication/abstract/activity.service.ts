import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {UserActivityDto} from "../../../dto/activity/user-activity-dto";
import {MyRecipesActivityDto} from "../../../dto/activity/my-recipes-activity-dto";

@Injectable()
export abstract class ActivityService {
  abstract setLike(recipeId: number): Observable<void>;

  abstract deleteLike(recipeId: number): Observable<void>;


  abstract setStar(recipeId: number): Observable<void>;

  abstract deleteStar(recipeId: number): Observable<void>;

  abstract getUserActivity(dto: MyRecipesActivityDto): Observable<UserActivityDto>;
}
