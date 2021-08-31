import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogManagerService} from "../../core/services/managers/dialog-manager.service";
import {BaseSearchManagerService} from "../../core/services/managers/search/base-search-manager.service";

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss'],
})
export class RecipesComponent implements OnInit {

  constructor(private router: Router,
              private tokenManager: AuthTokenManagerService,
              private activatedRoute: ActivatedRoute,
              private dialogManagerService: DialogManagerService,
              private searchManagerService: BaseSearchManagerService) {
  }

  ngOnInit() {
    const searchString = this.activatedRoute.snapshot.queryParams['searchString'];
    if (searchString != null) {
      this.searchManagerService.setString(searchString);
      this.searchManagerService.search();
    }
  }

  addNewRecipe() {
    if (this.tokenManager.isAuthorized) {
      this.router.navigate(["/recipe/new"]);
    } else {
      this.dialogManagerService.openAuthDialog("Добавлять рецепты могут только зарегистрированные пользователи.");
    }
  }
}
