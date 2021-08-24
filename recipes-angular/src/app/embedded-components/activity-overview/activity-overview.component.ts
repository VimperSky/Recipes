import {Component} from '@angular/core';

@Component({
  selector: 'app-activity-overview',
  templateUrl: './activity-overview.component.html',
  styleUrls: ['./activity-overview.component.scss']
})
export class ActivityOverviewComponent {

  constructor() { }

  // Пока что делать смысла нет, без ActivityController это работать не будет
  public ownRecipes: number = 0;
  public likedRecipes: number = 0;
  public starredRecipes: number = 0;
}
