import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {SuggestedTagsDto} from "../../../dto/tag/suggested-tags-dto";
import {FeaturedTagsDto} from "../../../dto/tag/featured-tags-dto";

@Injectable()
export abstract class TagsService {
  abstract getSuggestedSearchTags(): Observable<SuggestedTagsDto>;
  abstract getFeaturedTags(): Observable<FeaturedTagsDto>;
}
