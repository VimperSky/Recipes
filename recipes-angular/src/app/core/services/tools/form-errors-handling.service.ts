import {Injectable} from '@angular/core';
import {HttpErrorResponse} from "@angular/common/http";
import {AbstractControl} from "@angular/forms";
import {ValidationProblemDetails} from "../../dto/base/validation-problem-details";

@Injectable({
  providedIn: 'root'
})
export class FormErrorsHandlingService {
  setValidationErrors(error: HttpErrorResponse, formControlsMap: Map<string, AbstractControl>) {
    let problemDetails: ValidationProblemDetails = JSON.parse(JSON.stringify(error.error));
    for (let err of Object.keys(problemDetails.errors)) {
      let val = formControlsMap.get(err);
      if (val) {
        val.setErrors({notValid: true});
      }
    }
  }
}
