import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-recipe-add-edit',
  templateUrl: './recipe-add-edit.component.html',
  styleUrls: ['./recipe-add-edit.component.scss']
})
export class RecipeAddEditComponent implements OnInit {

  image: string | SafeResourceUrl | null = null;
  readonly acceptImageTypes: string = "image/png, image/jpeg";

  recipeForm = this.fb.group({
    recipeName: new FormControl('', [
      Validators.required
    ]),
    recipeDescription: new FormControl('', [
      Validators.maxLength(150)
    ]),
    cookingTime: new FormControl('', [
      Validators.max(999)
    ]),
    portions: new FormControl('', [
      Validators.max(999)
    ]),
    ingredients: this.fb.array([]),
    steps: this.fb.array([])
  })

  get ingredients(): FormArray {
    return this.recipeForm.controls['ingredients'] as FormArray;
  }

  get steps(): FormArray {
    return this.recipeForm.controls['steps'] as FormArray;
  }

  constructor(private sanitizer: DomSanitizer, private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.addIngredient()
    this.addStep()
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

  publish() {
    this.recipeForm.markAllAsTouched();
    if (this.recipeForm.invalid)
      return;
  }

  addIngredient() {
    const ingredientBlock = this.fb.group({
      header: ['', [Validators.required, Validators.maxLength(24)]],
      value: ['', [Validators.required, Validators.maxLength(500)]]
    })
    this.ingredients.push(ingredientBlock)
  }

  addStep() {
    const step = this.fb.group({
      value: ['', [Validators.required, Validators.maxLength(500)]]
    })
    this.steps.push(step)
  }


  deleteIngredient(id: number) {
    this.ingredients.removeAt(id);
  }

  deleteStep(id: number) {
    this.steps.removeAt(id);
  }
}
