import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss']
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
