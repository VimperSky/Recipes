import {Component, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {BaseRecipesManagerService} from "../../core/services/managers/recipes/base-recipes-manager.service";
import {AllRecipesManagerService} from "../../core/services/managers/recipes/all-recipes-manager.service";
import {TagsService} from "../../core/services/communication/abstract/tags.service";
import {SuggestedTags} from "../../core/dto/tag/suggested-tags";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  searchString = new FormControl('', []);
  public recipesManager: AllRecipesManagerService;

  public tags: string[] | undefined;

  constructor(recipesManager: BaseRecipesManagerService, private tagsService: TagsService) {
    this.recipesManager = recipesManager as AllRecipesManagerService
  }

  ngOnInit(): void {
    this.tagsService.getSuggestedSearchTags().subscribe((result: SuggestedTags) => {
      this.tags = result.tagValues;
    })
  }

  search() {
    this.recipesManager.search(this.searchString.value);
  }

  selectTag(tag: string) {
    this.searchString.setValue(tag);
    this.search();
  }
}
