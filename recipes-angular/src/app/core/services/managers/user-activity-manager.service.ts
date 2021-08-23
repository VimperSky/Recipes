import {Injectable, OnInit} from "@angular/core";
import {ActivityService} from "../communication/abstract/activity.service";
import {UserActivity} from "../../dto/user/user-activity";
import {Observable, of, Subject} from "rxjs";
import {AuthTokenManagerService} from "./auth-token-manager.service";

@Injectable({
  providedIn: 'root'
})
export class UserActivityManagerService implements OnInit {
  constructor(private activityService: ActivityService,
              private authTokenManagerService: AuthTokenManagerService) {}

  private userActivity: UserActivity | null = null;
  private userActivitySub = new Subject<UserActivity | null>();

  public get currentActivity(): UserActivity | null {
    return this.userActivity;
  }

  public activityChanged = this.userActivitySub.asObservable();

  public ngOnInit(): void {
    this.authTokenManagerService.authChanged.subscribe((value: boolean) => {
      if (value) {
        this.activityService.getUserActivity().subscribe((activity: UserActivity) => {
          this.userActivity = activity;
          this.raiseActivityChange();
        });
      }
      else {
        this.userActivity = null;
        this.raiseActivityChange();
      }
    })
  }

  private raiseActivityChange() {
    this.userActivitySub.next(this.userActivity);
  }

}
