import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-activity-overview',
  templateUrl: './activity-overview.component.html',
  styleUrls: ['./activity-overview.component.scss']
})
export class ActivityOverviewComponent implements OnInit {

  constructor() { }

  // Пока что делать смысла нет, без ActivityController это работать не будет
  ownRecipes: number = 0;
  likedRecipes: number = 0;
  starredRecipes: number = 0;

  ngOnInit(): void {
  }

}
