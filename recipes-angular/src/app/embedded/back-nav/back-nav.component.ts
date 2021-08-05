import {Component, OnInit} from '@angular/core';
import {Location} from '@angular/common';

@Component({
  selector: 'app-back-nav',
  templateUrl: './back-nav.component.html',
  styleUrls: ['./back-nav.component.scss']
})
export class BackNavComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit(): void {
  }

  goBack() {
    this.location.back();
  }

}
