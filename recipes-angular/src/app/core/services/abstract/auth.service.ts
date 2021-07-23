import { Injectable } from '@angular/core';

@Injectable()
export abstract class AuthService {
  abstract register(): void;
  abstract auth(): void;
}
