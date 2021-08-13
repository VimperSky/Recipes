import { Component, OnInit } from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {FormArray, FormBuilder, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeService} from "../../core/services/communication/abstract/recipe.service";
import {RecipeDetail} from "../../core/dto/recipe/recipe-detail";
import {environment} from "../../../environments/environment";
import {RecipeCreate} from "../../core/dto/recipe/recipe-create";
import {Ingredient} from "../../core/dto/recipe/ingredient";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpErrorResponse} from "@angular/common/http";
import {RecipeEdit} from "../../core/dto/recipe/recipe-edit";
import {DialogDisplayService} from "../../core/services/tools/dialog-display.service";
import {ProblemDetails} from "../../core/dto/base/problem-details";
import {ErrorHandlingService} from "../../core/services/tools/error-handling.service";

@Component({
  selector: 'app-recipe-add-edit',
  templateUrl: './recipe-add-edit.component.html',
  styleUrls: ['../../shared-styles/form-styles.scss', './recipe-add-edit.component.scss']
})
export class RecipeAddEditComponent implements OnInit {

  image: string | SafeResourceUrl | undefined;
  private file: File | undefined;
  readonly acceptImageTypes: string = "image/png, image/jpeg";

  recipeName = this.fb.control('', [
    Validators.required,
    Validators.pattern('.*[a-zA-Zа-яА-Я].*') // хотя бы один символ из алфавита (рус англ)
  ]);

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

  get isEdit(): boolean {
    return this.id != null;
  }

  constructor(private sanitizer: DomSanitizer,
              private fb: FormBuilder,
              private recipeService: RecipeService,
              private snackBar: MatSnackBar,
              private router: Router,
              private errorHandlingService: ErrorHandlingService,
              activatedRoute: ActivatedRoute) {
    this.id = activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    if (this.isEdit) {
      this.recipeService.detail(this.id!).subscribe((result: RecipeDetail) => {

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

    this.file = file;
    const imagePath = URL.createObjectURL(file);
    this.image = this.sanitizer.bypassSecurityTrustResourceUrl(imagePath);
  }

  deleteImage() {
    if (this.image)
    {
      this.image = undefined;
    }
  }

  private finalizeRecipeProcessing(recipeId: number | undefined = undefined) {
    this.snackBar.open(!recipeId ? 'Рецепт был успешно отредактирован!' : 'Рецепт был успешно создан!',
      'ОК', {duration: 5000
    });
    if (!recipeId)
      recipeId = this.id;
    this.router.navigate([`/recipe/detail/${recipeId}`])
  }

  publish() {
    this.recipeForm.markAllAsTouched();
    if (this.recipeForm.invalid)
      return;

    let steps: string[] = [];
    for (let step of this.steps.controls) {
      steps.push(step.value.value);
    }

    let ingredients: Ingredient[] = [];
    for (let ingredient of this.ingredients.controls) {
      let ingredientDto: Ingredient = {
        header: ingredient.value.header,
        value: ingredient.value.value
      }
      ingredients.push(ingredientDto);
    }

    if (this.isEdit) {
      const dto: RecipeEdit = {
        id: this.id as number,
        name: this.recipeName.value,
        description: this.recipeDescription.value,
        steps: steps,
        ingredients: ingredients,
        cookingTimeMin: this.cookingTime.value == "" ? 0 : this.cookingTime.value,
        portions: this.portions.value == "" ? 0: this.portions.value
      };

      this.recipeService.edit(dto).subscribe(() => {
        if (this.file) {
          this.recipeService.uploadImage(this.id as number, this.file).subscribe(() => {
            this.finalizeRecipeProcessing();
          }, (error: HttpErrorResponse) => {
            this.errorHandlingService.openErrorDialog(error, "При загрузке изображения произошла неопознанная ошибка.");
          });
        }
        else {
          this.finalizeRecipeProcessing();
        }

      }, (error: HttpErrorResponse) => {
        this.errorHandlingService.openErrorDialog(error, "При редактировании рецепта произошла неопознанная ошибка.");
      })
    }
    else {
      const dto: RecipeCreate = {
        name: this.recipeName.value,
        description: this.recipeDescription.value,
        steps: steps,
        ingredients: ingredients,
        cookingTimeMin: this.cookingTime.value == "" ? 0 : this.cookingTime.value,
        portions: this.portions.value == "" ? 0: this.portions.value
      };

      this.recipeService.create(dto).subscribe((id: number) => {
        if (this.file)
          this.recipeService.uploadImage(id, this.file).subscribe(() => {
            this.finalizeRecipeProcessing(id);
          }, (error: HttpErrorResponse) => {
            this.errorHandlingService.openErrorDialog(error, "При загрузке изображения произошла неопознанная ошибка.");
          });
        else {
          this.finalizeRecipeProcessing(id);
        }

      }, (error: HttpErrorResponse) => {
        this.errorHandlingService.openErrorDialog(error, "При создании рецепта произошла неопознанная ошибка.");
      })
    }
  }

  addIngredient(header: string = "", value: string = "") {
    const ingredientBlock = this.fb.group({
      header: this.fb.control(header, [Validators.maxLength(24)]),
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
