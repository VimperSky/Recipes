import { Injectable } from '@angular/core';
import {ImageUploadService} from "../abstract/image-upload.service";
import {Observable, of} from "rxjs";
import {DomSanitizer} from "@angular/platform-browser";

@Injectable()
export class StubImageUploadService extends ImageUploadService {
  constructor(private sanitizer: DomSanitizer) {
    super();
  }

  uploadFile(file: File): Observable<string> {
    const imagePath = URL.createObjectURL(file);
    return of(imagePath);
  }
}
