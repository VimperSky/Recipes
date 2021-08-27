import {Injectable} from '@angular/core';
import {FormControl} from "@angular/forms";
import {BaseSearchManagerService} from "./base-search-manager.service";
import {Router} from "@angular/router";

@Injectable()
export class RedirectSearchManagerService extends BaseSearchManagerService {
  public searchString = new FormControl('', []);

  constructor(private router: Router) {
    super();
  }

  public setString(value: string) {
    this.searchString.setValue(value);
  }

  public search() {
    this.router.navigate(['/recipes'], {queryParams: {searchString: this.searchString.value}})
  }
}

