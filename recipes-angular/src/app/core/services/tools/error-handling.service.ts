import {Injectable} from '@angular/core';
import {HttpErrorResponse} from "@angular/common/http";
import {AbstractControl} from "@angular/forms";
import {ValidationProblemDetails} from "../../dto/base/validation-problem-details";
import {ProblemDetails} from "../../dto/base/problem-details";
import {DialogManagerService} from "../managers/dialog-manager.service";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {
  constructor(private dialogManagerService: DialogManagerService) {
  }

  public setValidationErrors(error: HttpErrorResponse, formControlsMap: Map<string, AbstractControl>) {
    let problemDetails: ValidationProblemDetails = JSON.parse(JSON.stringify(error.error));
    for (let err of Object.keys(problemDetails.errors)) {
      let val = formControlsMap.get(err);
      if (val) {
        val.setErrors({notValid: true});
      }
    }
  }

  public openErrorDialog(error: HttpErrorResponse, elseErrorText: string) {
    // Unauthorized
    if (error.status == 401) {
      this.dialogManagerService.openAuthDialog();
      return;
    }

    if (error.error) {
      let problemDetails: ProblemDetails = JSON.parse(JSON.stringify(error.error));
      if (problemDetails.detail) {
        this.dialogManagerService.openErrorDialog(problemDetails.detail);
        return;
      }
    }

    this.dialogManagerService.openErrorDialog(elseErrorText)
  }
}
