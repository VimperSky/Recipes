import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RecipesComponent} from "./pages/recipes/recipes.component";
import {RecipeDetailComponent} from "./pages/recipe-detail/recipe-detail.component";

const routes: Routes = [
  {
    path: 'recipes',
    component: RecipesComponent
  },
  {
    path: 'recipe/:id',
    component: RecipeDetailComponent
  },
  {
    path: '**',
    redirectTo: "/recipes",
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
