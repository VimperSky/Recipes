import {Injectable} from '@angular/core';
import {HttpErrorResponse} from "@angular/common/http";
import {AbstractControl} from "@angular/forms";
import {ValidationProblemDetails} from "../../dto/base/validation-problem-details";
import {MatDialog} from "@angular/material/dialog";
import {ProblemDetails} from "../../dto/base/problem-details";
import {ErrorComponent} from "../../../embedded-components/error/error.component";
import {DialogDisplayService} from "./dialog-display.service";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {
  constructor(private dialogDisplayService: DialogDisplayService) {}

  setValidationErrors(error: HttpErrorResponse, formControlsMap: Map<string, AbstractControl>) {
    let problemDetails: ValidationProblemDetails = JSON.parse(JSON.stringify(error.error));
    for (let err of Object.keys(problemDetails.errors)) {
      let val = formControlsMap.get(err);
      if (val) {
        val.setErrors({notValid: true});
      }
    }
  }

  openErrorDialog(error: HttpErrorResponse, elseErrorText: string) {
    if (error.status == 401) {
      this.dialogDisplayService.openAuthDialog();
    } else if (error.error) {
      let problemDetails: ProblemDetails = JSON.parse(JSON.stringify(error.error));
      this.dialogDisplayService.openErrorDialog(problemDetails.detail);
    }
    else {
      this.dialogDisplayService.openErrorDialog(elseErrorText)
    }
  }
}
