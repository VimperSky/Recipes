import {Component, OnInit} from '@angular/core';
import {TagsService} from "../../core/services/communication/abstract/tags.service";
import {SuggestedTagsDto} from "../../core/dto/tag/suggested-tags-dto";
import {SearchManagerService} from "../../core/services/managers/recipes/search-manager.service";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  public tags: string[] | undefined;

  constructor(public searchService: SearchManagerService, private tagsService: TagsService) {}

  ngOnInit(): void {
    this.tagsService.getSuggestedSearchTags().subscribe((result: SuggestedTagsDto) => {
      this.tags = result.tagValues;
    })
  }

  search() {
    this.searchService.search();
  }

  selectTag(tag: string) {
    this.searchService.setString(tag);
    this.search();
  }
}
