import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
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
              private dialogDisplayService: DialogDisplayService,
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
    }
    else {
      this.dialogDisplayService.openAuthDialog();
    }
  }
}
