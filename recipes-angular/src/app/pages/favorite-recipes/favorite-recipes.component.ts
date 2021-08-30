import {Component} from '@angular/core';
import {RecipesManagerService} from "../../core/services/managers/recipes/recipes-manager.service";
import {FavoriteRecipesManagerService} from "../../core/services/managers/recipes/favorite-recipes-manager.service";

@Component({
  selector: 'app-favorite-recipes',
  templateUrl: './favorite-recipes.component.html',
  styleUrls: ['./favorite-recipes.component.scss'],
  providers: [
    {
      provide: RecipesManagerService,
      useClass: FavoriteRecipesManagerService
    },
  ]
})
export class FavoriteRecipesComponent {
  constructor() {
  }
}
