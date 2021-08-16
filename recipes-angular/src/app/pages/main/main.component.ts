import { Component, OnInit } from '@angular/core';
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";

@Component({
  selector: 'app-main-page',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor(private dialogDisplay: DialogDisplayService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              authTokenManager: AuthTokenManagerService) {
    const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];

    if (authTokenManager.isAuthorized || !returnUrl) {
      router.navigate(['/recipes'])
      return;
    }

    this.dialogDisplay.openAuthDialog("Доступ к этой странице имеют только зарегистрированные пользователи.");
    authTokenManager.authChanged.subscribe(() => {
      if (authTokenManager.isAuthorized)
      {
        router.navigate([returnUrl])
      }
    })
  }

  ngOnInit(): void {
  }
}
