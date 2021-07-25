import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {HeaderComponent} from './embedded/header/header.component';
import {RecipeSearchComponent} from './embedded/recipe-search/recipe-search.component';
import {RecipesComponent} from './pages/recipes/recipes.component';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {RecipeListComponent} from './embedded/recipe-list/recipe-list.component';
import {MatIconModule} from "@angular/material/icon";
import {FooterComponent} from './embedded/footer/footer.component';
import {ReactiveFormsModule} from "@angular/forms";
import {RecipesService} from "./core/services/abstract/recipes.service";
import {RecipeDetailComponent} from './pages/recipe-detail/recipe-detail.component';
import {RecipeService} from "./core/services/abstract/recipe.service";
import {RecipePreviewComponent} from './embedded/recipe-preview/recipe-preview.component';
import {BackNavComponent} from './embedded/back-nav/back-nav.component';
import {ApiRecipeService} from "./core/services/api/api-recipe.service";
import {ApiRecipesService} from "./core/services/api/api-recipes.service";
import {HttpClientModule} from "@angular/common/http";
import {AuthHeaderComponent} from './embedded/auth-header/auth-header.component';
import {MatDividerModule} from "@angular/material/divider";
import {MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule} from "@angular/material/dialog";
import {LoginComponent} from './embedded/auth/login/login.component';
import {RegisterComponent} from './embedded/auth/register/register.component';
import {AuthService} from "./core/services/abstract/auth.service";
import {ApiAuthService} from "./core/services/api/api-auth.service";

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
    BackNavComponent,
    AuthHeaderComponent,
    LoginComponent,
    RegisterComponent
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
    HttpClientModule,
    MatDividerModule,
    MatDialogModule
  ],
  providers: [
    {
      provide: RecipesService,
      useClass: ApiRecipesService
    },
    {
      provide: RecipeService,
      useClass: ApiRecipeService
    },
    {
      provide: AuthService,
      useClass: ApiAuthService
    },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: {hasBackdrop: false}
    },
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: {appearance: 'outline'}
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
