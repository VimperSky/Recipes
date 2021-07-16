import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule} from "@angular/material/toolbar";
import { MatButtonModule} from "@angular/material/button";
import { HeaderComponent } from './embedded/header/header.component';
import { RecipeSearchComponent } from './embedded/recipe-search/recipe-search.component';
import { RecipesComponent } from './pages/recipes/recipes.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import { RecipeListComponent } from './embedded/recipe-list/recipe-list.component';
import {MatIconModule} from "@angular/material/icon";
import { FooterComponent } from './embedded/footer/footer.component';
import {ReactiveFormsModule} from "@angular/forms";
import {RecipesService} from "./core/services/recipes.service";
import {TestRecipesService} from "./core/services/test/test-recipes.service";
import { RecipeDetailComponent } from './pages/recipe-detail/recipe-detail.component';
import {RecipeService} from "./core/services/recipe.service";
import {TestRecipeService} from "./core/services/test/test-recipe.service";
import { RecipePreviewComponent } from './embedded/recipe-preview/recipe-preview.component';
import { BackNavComponent } from './embedded/back-nav/back-nav.component';
import {RouterModule} from "@angular/router";
import {ApiRecipeService} from "./core/services/api/api-recipe.service";
import {ApiRecipesService} from "./core/services/api/api-recipes.service";
import {HttpClientModule} from "@angular/common/http";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    RecipeSearchComponent,
    RecipesComponent,
    RecipeListComponent,
    FooterComponent,
    RecipeDetailComponent,
    RecipePreviewComponent,
    BackNavComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: RecipesService,
      useClass: ApiRecipesService
    },
    {
      provide: RecipeService,
      useClass: TestRecipeService
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
