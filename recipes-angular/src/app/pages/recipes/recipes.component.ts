import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {MatDialog} from "@angular/material/dialog";
import {AuthComponent} from "../../embedded-components/auth/auth/auth.component";

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss']
})
export class RecipesComponent implements OnInit {

  constructor(private router: Router,
              private tokenManager: AuthTokenManagerService,
              private dialog: MatDialog) {
  }

  ngOnInit() {

  }

  addNewRecipe() {
    if (this.tokenManager.isAuthorized) {
      this.router.navigate(["/recipe/new"]);
    }
    else {
      this.dialog.open(AuthComponent, {
        panelClass: 'auth-dialog-container'
      });
    }

  }
}
