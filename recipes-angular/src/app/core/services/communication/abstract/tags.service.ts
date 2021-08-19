import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {SuggestedTags} from "../../../dto/tag/suggested-tags";

@Injectable()
export abstract class TagsService {
  abstract  getSuggestedSearchTags(): Observable<SuggestedTags>;
}
