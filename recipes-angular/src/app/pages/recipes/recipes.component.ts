import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {BaseRecipesManagerService} from "../../core/services/managers/recipes/base-recipes-manager.service";
import {AllRecipesManagerService} from "../../core/services/managers/recipes/all-recipes-manager.service";
import {SearchManagerService} from "../../core/services/managers/recipes/search-manager.service";

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss'],
  providers: [
    {
      provide: SearchManagerService,
    },
    {
      provide: BaseRecipesManagerService,
      useClass: AllRecipesManagerService
    },

  ]
})
export class RecipesComponent implements OnInit {

  constructor(private router: Router,
              private tokenManager: AuthTokenManagerService,
              private dialogDisplayService: DialogDisplayService) {
  }

  ngOnInit() {

  }

  addNewRecipe() {
    if (this.tokenManager.isAuthorized) {
      this.router.navigate(["/recipe/new"]);
    }
    else {
      this.dialogDisplayService.openAuthDialog();
    }

  }
}
