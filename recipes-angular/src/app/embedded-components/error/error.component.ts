import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

export interface ErrorDialogData {
  errorText: string;
}

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['../../shared-styles/form-styles.scss', '../../shared-styles/dialog-styles.scss', './error.component.scss']
})
export class ErrorComponent {

  public errorText: string;

  constructor(@Inject(MAT_DIALOG_DATA) data: ErrorDialogData) {
    this.errorText = data.errorText;
  }
}
