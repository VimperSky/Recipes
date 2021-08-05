import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RecipesComponent} from "./pages/recipes/recipes.component";
import {RecipeDetailComponent} from "./pages/recipe-detail/recipe-detail.component";
import {JwtModule} from "@auth0/angular-jwt";
import {environment} from "../environments/environment";

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

function tokenGetter() {
  return localStorage.getItem(environment.jwtToken);
}

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled'
    }),
    JwtModule.forRoot({config: {
      tokenGetter: tokenGetter
    }})
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
