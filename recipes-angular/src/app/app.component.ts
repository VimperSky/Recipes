import {Component, OnInit} from '@angular/core';
import {CredentialsValidatorService} from "./core/services/tools/credentials-validator.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(private credentialsValidator: CredentialsValidatorService) {}

  ngOnInit() {
    this.credentialsValidator.validate();
  }
}
