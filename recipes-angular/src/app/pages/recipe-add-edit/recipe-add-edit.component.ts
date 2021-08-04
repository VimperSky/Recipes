import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {FormArray, FormBuilder, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../core/services/communication/abstract/recipe.service";
import {RecipeDetail} from "../../core/dto/recipe/recipe-detail";
import {RecipePreview} from "../../core/dto/recipe/recipe-preview";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-recipe-add-edit',
  templateUrl: './recipe-add-edit.component.html',
  styleUrls: ['../../shared-styles/form-styles.scss', './recipe-add-edit.component.scss']
})
export class RecipeAddEditComponent implements OnInit {

  image: string | SafeResourceUrl | null = null;
  readonly acceptImageTypes: string = "image/png, image/jpeg";

  recipeName = this.fb.control('', [Validators.required]);
  recipeDescription = this.fb.control('', [Validators.maxLength(150)])
  cookingTime = this.fb.control('', [Validators.max(999)]);
  portions = this.fb.control('', [Validators.max(999)]);
  ingredients: FormArray = this.fb.array([]);
  steps = this.fb.array([]);

  recipeForm = this.fb.group({
    recipeName: this.recipeName,
    recipeDescription: this.recipeDescription,
    cookingTime: this.cookingTime,
    portions: this.portions,
    ingredients: this.ingredients,
    steps: this.steps
  })

  id: number | undefined;

  constructor(private sanitizer: DomSanitizer,
              private fb: FormBuilder,
              private recipeService: RecipeService,
              route: ActivatedRoute) {
    this.id = route.snapshot.params['id'];
  }

  ngOnInit(): void {
    if (this.id) {
      this.recipeService.detail(this.id).subscribe((result: RecipeDetail) => {

        this.recipeName.setValue(result.name);
        this.recipeDescription.setValue(result.description);
        this.cookingTime.setValue(result.cookingTimeMin);
        this.portions.setValue(result.portions);
        this.image = environment.backendUrl + "/" + result.imagePath;

        for (const ingredient of result.ingredients) {
          this.addIngredient(ingredient.header, ingredient.value)
        }

        for (const step of result.steps) {
          this.addStep(step)
        }
      })
    }
    else {
      this.addIngredient()
      this.addStep()
    }
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

  addIngredient(header: string = "", value: string = "") {
    const ingredientBlock = this.fb.group({
      header: this.fb.control(header, [Validators.required, Validators.maxLength(24)]),
      value: this.fb.control(value, [Validators.required, Validators.maxLength(500)]),
    })
    this.ingredients.push(ingredientBlock)
  }


  addStep(value: string = "") {
    const step = this.fb.group({
      value: [value, [Validators.required, Validators.maxLength(500)]]
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
