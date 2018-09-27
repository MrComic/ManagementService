import { Injectable } from '@angular/core';
import {CanActivate} from "@angular/router";
import {AuthService} from "./AuthService";

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService) { }

  canActivate(): boolean {
      return false;
  }
}

