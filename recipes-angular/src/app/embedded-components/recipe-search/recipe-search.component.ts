import {Component, Input, OnInit} from '@angular/core';
import {TagsService} from "../../core/services/communication/abstract/tags.service";
import {SuggestedTagsDto} from "../../core/dto/tag/suggested-tags-dto";
import {BaseSearchManagerService} from "../../core/services/managers/search/base-search-manager.service";

@Component({
  selector: 'app-recipe-search',
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.scss']
})
export class RecipeSearchComponent implements OnInit {

  @Input()
  public isExtended: boolean = false;

  public tags: string[] | undefined;

  constructor(public searchService: BaseSearchManagerService, private tagsService: TagsService) {
  }

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
