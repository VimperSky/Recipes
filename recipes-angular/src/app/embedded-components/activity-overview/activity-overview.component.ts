import {Component, OnInit} from '@angular/core';
import {UserService} from "../../core/services/communication/abstract/user.service";
import {UserStats} from "../../core/dto/user/user-stats";

@Component({
  selector: 'app-activity-overview',
  templateUrl: './activity-overview.component.html',
  styleUrls: ['./activity-overview.component.scss']
})
export class ActivityOverviewComponent implements OnInit {

  public ownRecipes: number = 0;
  public likedRecipes: number = 0;
  public starredRecipes: number = 0;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUserStats().subscribe((stats: UserStats) => {
      this.ownRecipes = stats.recipesCount;
      this.starredRecipes = stats.starsCount;
      this.likedRecipes = stats.likesCount;
    })
  }


}
