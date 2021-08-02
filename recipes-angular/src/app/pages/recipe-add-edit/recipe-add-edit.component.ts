import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";

@Component({
  selector: 'app-recipe-add-edit',
  templateUrl: './recipe-add-edit.component.html',
  styleUrls: ['./recipe-add-edit.component.scss']
})
export class RecipeAddEditComponent implements OnInit {

  image: string | SafeResourceUrl | null = null;
  readonly acceptImageTypes: string = "image/png, image/jpeg";

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
  }

  onFileSelected(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    if (!files)
      return;

    const file = files[0];
    if (!file)
      return;

    const imagePath = URL.createObjectURL(file);
    this.image = this.sanitizer.bypassSecurityTrustResourceUrl(imagePath);

    console.log(this.image)
    // this.imageUploadService.uploadFile(file).subscribe((data: string) => {
    //   this.image = data;
    // })
  }

  deleteImage() {
    if (this.image)
    {
      this.image = null;
      console.log('delete yeah')
    }
  }
}
