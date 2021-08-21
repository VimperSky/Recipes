import {Injectable} from "@angular/core";
import {FormControl} from "@angular/forms";

@Injectable()
export abstract class BaseSearchManagerService {
  public abstract searchString = new FormControl('', []);

  public abstract setString(value: string): void;

  public abstract search(): void;
}

