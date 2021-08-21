import { Component, OnInit } from '@angular/core';
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";
import {BaseSearchManagerService} from "../../core/services/managers/search/base-search-manager.service";
import {RedirectSearchManagerService} from "../../core/services/managers/search/redirect-search-manager.service";

@Component({
  selector: 'app-main-page',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [
    {
      provide: BaseSearchManagerService,
      useClass: RedirectSearchManagerService
    },
  ]
})
export class MainComponent {
  constructor(private dialogDisplay: DialogDisplayService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              authTokenManager: AuthTokenManagerService) {

    const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];
    if (returnUrl) {
      if (authTokenManager.isAuthorized) {
        router.navigate([returnUrl]);
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
  }


}
