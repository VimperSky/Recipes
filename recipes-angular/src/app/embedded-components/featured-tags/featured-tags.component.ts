import {Component, Input, OnInit} from '@angular/core';
import {TagsService} from "../../core/services/communication/abstract/tags.service";
import {FeaturedTagsDto} from "../../core/dto/tag/featured-tags-dto";
import {TagDto} from "../../core/dto/tag/tag-dto";
import {environment} from "../../../environments/environment";
import {SearchManagerService} from "../../core/services/managers/recipes/search-manager.service";

@Component({
  selector: 'app-featured-tags',
  templateUrl: './featured-tags.component.html',
  styleUrls: ['./featured-tags.component.scss']
})
export class FeaturedTagsComponent implements OnInit {

  @Input()
  isFull: boolean = false;

  tags: TagDto[] | undefined;

  getImagePath(tag: TagDto): string {
    return environment.backendUrl + "/" + tag.icon;
  }

  constructor(private tagsService: TagsService, private searchService: SearchManagerService) {}

  ngOnInit(): void {
    this.tagsService.getFeaturedTags().subscribe((dto: FeaturedTagsDto) => {
      this.tags = dto.tags;
    })
  }

  searchForTag(tag: TagDto) {
    this.searchService.setString(tag.value);
    this.searchService.search();
  }

}
