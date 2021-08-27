import {Component, Input, OnInit} from '@angular/core';
import {TagsService} from "../../core/services/communication/abstract/tags.service";
import {FeaturedTagsDto} from "../../core/dto/tag/featured-tags-dto";
import {TagDto} from "../../core/dto/tag/tag-dto";
import {environment} from "../../../environments/environment";
import {BaseSearchManagerService} from "../../core/services/managers/search/base-search-manager.service";

@Component({
  selector: 'app-featured-tags',
  templateUrl: './featured-tags.component.html',
  styleUrls: ['./featured-tags.component.scss']
})
export class FeaturedTagsComponent implements OnInit {

  @Input()
  public isExtended: boolean = false;
  public tags: TagDto[] | undefined;

  constructor(private tagsService: TagsService, private searchService: BaseSearchManagerService) {
  }

  public getImagePath(tag: TagDto): string {
    return environment.backendUrl + "/" + tag.icon;
  }

  public ngOnInit(): void {
    this.tagsService.getFeaturedTags().subscribe((dto: FeaturedTagsDto) => {
      this.tags = dto.tags;
    })
  }

  public searchForTag(tag: TagDto) {
    this.searchService.setString(tag.value);
    this.searchService.search();
  }

}
