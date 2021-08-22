import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RecipesComponent} from "./pages/recipes/recipes.component";
import {RecipeDetailComponent} from "./pages/recipe-detail/recipe-detail.component";
import {RecipeAddEditComponent} from "./pages/recipe-add-edit/recipe-add-edit.component";
import {ProfileComponent} from "./pages/profile/profile.component";
import {AuthGuard} from "./core/guards/auth.guard";
import {MainComponent} from "./pages/main/main.component";

const routes: Routes = [
  {
    path: 'recipes',
    component: RecipesComponent
  },
  {
    path: 'recipe/new',
    component: RecipeAddEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'recipe/edit/:id',
    component: RecipeAddEditComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'recipe/detail/:id',
    component: RecipeDetailComponent
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    component: MainComponent,
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
export class AppRoutingModule {
}
