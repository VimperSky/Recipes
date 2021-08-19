import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {TagsService} from "../abstract/tags.service";
import {Observable} from "rxjs";
import {environment} from "../../../../../environments/environment";
import {SuggestedTags} from "../../../dto/tag/suggested-tags";

const basePath: string = "/api/tags"

@Injectable()
export class ApiTagsService extends TagsService {

  constructor(private http: HttpClient) {
    super();
  }

  getSuggestedSearchTags(): Observable<SuggestedTags> {
    return this.http.get<SuggestedTags>(environment.backendUrl + basePath + "/suggested");
  }
}
