import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {HeaderComponent} from './embedded-components/header/header.component';
import {RecipeSearchComponent} from './embedded-components/recipe-search/recipe-search.component';
import {RecipesComponent} from './pages/recipes/recipes.component';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {RecipeListComponent} from './embedded-components/recipe-list/recipe-list.component';
import {MatIconModule} from "@angular/material/icon";
import {FooterComponent} from './embedded-components/footer/footer.component';
import {ReactiveFormsModule} from "@angular/forms";
import {RecipesService} from "./core/services/communication/abstract/recipes.service";
import {RecipeDetailComponent} from './pages/recipe-detail/recipe-detail.component';
import {RecipeService} from "./core/services/communication/abstract/recipe.service";
import {RecipePreviewComponent} from './embedded-components/recipe-preview/recipe-preview.component';
import {BackNavComponent} from './embedded-components/back-nav/back-nav.component';
import {ApiRecipeService} from "./core/services/communication/api/api-recipe.service";
import {ApiRecipesService} from "./core/services/communication/api/api-recipes.service";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthHeaderComponent} from './embedded-components/auth-header/auth-header.component';
import {MatDividerModule} from "@angular/material/divider";
import {MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule} from "@angular/material/dialog";
import {LoginComponent} from './embedded-components/auth/login/login.component';
import {RegisterComponent} from './embedded-components/auth/register/register.component';
import {UserService} from "./core/services/communication/abstract/user.service";
import {ApiUserService} from "./core/services/communication/api/api-user.service";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {RecipeAddEditComponent} from './pages/recipe-add-edit/recipe-add-edit.component';
import {AutosizeModule} from "ngx-autosize";
import {JwtModule} from "@auth0/angular-jwt";
import {AuthComponent} from './embedded-components/auth/auth/auth.component';
import {ErrorComponent} from './embedded-components/error/error.component';
import {ProfileComponent} from './pages/profile/profile.component';
import {ActivityOverviewComponent} from './embedded-components/activity-overview/activity-overview.component';
import {MainComponent} from "./pages/main/main.component";
import {MatChipsModule} from "@angular/material/chips";
import {TagsService} from "./core/services/communication/abstract/tags.service";
import {ApiTagsService} from "./core/services/communication/api/api-tags";
import {FeaturedTagsComponent} from './embedded-components/featured-tags/featured-tags.component';
import {MatRippleModule} from "@angular/material/core";
import {MainHeaderComponent} from './embedded-components/main-header/main-header.component';
import {BaseSearchManagerService} from "./core/services/managers/search/base-search-manager.service";
import {SearchManagerService} from "./core/services/managers/search/search-manager.service";
import {RecipesManagerService} from "./core/services/managers/recipes/recipes-manager.service";
import {AllRecipesManagerService} from "./core/services/managers/recipes/all-recipes-manager.service";
import {RecipeOfTheDayComponent} from './embedded-components/recipe-of-the-day/recipe-of-the-day.component';
import {ActivityService} from "./core/services/communication/abstract/activity.service";
import {ApiActivityService} from "./core/services/communication/api/api-activity.service";
import { FavoriteRecipesComponent } from './pages/favorite-recipes/favorite-recipes.component';
import {ApiInterceptor} from "./core/interceptors/api.interceptor";


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
    RegisterComponent,
    RecipeAddEditComponent,
    AuthComponent,
    ErrorComponent,
    ProfileComponent,
    ActivityOverviewComponent,
    MainComponent,
    FeaturedTagsComponent,
    MainHeaderComponent,
    RecipeOfTheDayComponent,
    ActivityOverviewComponent,
    ErrorComponent,
    FavoriteRecipesComponent
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
    MatDialogModule,
    MatSnackBarModule,
    AutosizeModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        throwNoTokenError: true,
      },
    }),
    MatChipsModule,
    MatRippleModule
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
      provide: UserService,
      useClass: ApiUserService
    },
    {
      provide: TagsService,
      useClass: ApiTagsService
    },
    {
      provide: ActivityService,
      useClass: ApiActivityService
    },
    {
      provide: BaseSearchManagerService,
      useClass: SearchManagerService
    },
    {
      provide: RecipesManagerService,
      useClass: AllRecipesManagerService
    },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: {hasBackdrop: false}
    },
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: {appearance: 'outline'}
    },


  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
