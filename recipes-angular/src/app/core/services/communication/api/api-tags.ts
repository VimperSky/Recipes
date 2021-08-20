import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {TagsService} from "../abstract/tags.service";
import {Observable} from "rxjs";
import {environment} from "../../../../../environments/environment";
import {SuggestedTagsDto} from "../../../dto/tag/suggested-tags-dto";
import {FeaturedTagsDto} from "../../../dto/tag/featured-tags-dto";

const basePath: string = "/api/tags"

@Injectable()
export class ApiTagsService extends TagsService {

  constructor(private http: HttpClient) {
    super();
  }

  getSuggestedSearchTags(): Observable<SuggestedTagsDto> {
    return this.http.get<SuggestedTagsDto>(environment.backendUrl + basePath + "/suggested");
  }

  getFeaturedTags(): Observable<FeaturedTagsDto> {
    return this.http.get<FeaturedTagsDto>(environment.backendUrl + basePath + "/featured");
  }
}
