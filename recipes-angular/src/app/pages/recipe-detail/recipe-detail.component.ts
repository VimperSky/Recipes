import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {RecipeDetail} from "../../core/dto/recipe/recipe-detail";
import {RecipeService} from "../../core/services/communication/abstract/recipe.service";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {

  recipeDetail: RecipeDetail | undefined;

  constructor(private activatedRoute: ActivatedRoute, private recipeService: RecipeService) {
  }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.params['id'];
    this.recipeService.detail(id).subscribe(result => {
      this.recipeDetail = result;
    });

  }
}
