import {Component} from '@angular/core';
import {Location} from '@angular/common';

@Component({
  selector: 'app-back-nav',
  templateUrl: './back-nav.component.html',
  styleUrls: ['./back-nav.component.scss']
})
export class BackNavComponent {

  constructor(private location: Location) {
  }

  goBack() {
    this.location.back();
  }

}
