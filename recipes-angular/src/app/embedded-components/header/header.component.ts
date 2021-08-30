import {Component} from '@angular/core';
import {AuthTokenManagerService} from "../../core/services/managers/auth-token-manager.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(public authManager: AuthTokenManagerService) {
  }
}
