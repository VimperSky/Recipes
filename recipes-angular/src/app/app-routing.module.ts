import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RecipesComponent} from "./pages/recipes/recipes.component";
import {RecipeDetailComponent} from "./pages/recipe-detail/recipe-detail.component";
import {RecipeAddEditComponent} from "./pages/recipe-add-edit/recipe-add-edit.component";
import {ProfileComponent} from "./pages/profile/profile.component";

const routes: Routes = [
  {
    path: 'recipes',
    component: RecipesComponent
  },
  {
    path: 'recipe/new',
    component: RecipeAddEditComponent,
  },
  {
    path: 'recipe/edit/:id',
    component: RecipeAddEditComponent,
  },
  {
    path: 'recipe/detail/:id',
    component: RecipeDetailComponent
  },
  {
    path: 'profile',
    component: ProfileComponent
  },
  {
    path: '**',
    redirectTo: "/recipes",
    pathMatch: 'full'
  },
];



@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled'
    }),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
