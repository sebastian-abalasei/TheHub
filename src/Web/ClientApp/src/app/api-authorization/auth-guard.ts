import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
} from '@angular/router';
import { map, Observable } from 'rxjs';
import {AuthState} from "./auth-state";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private authState: AuthState, private _router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    // save snapshop url
    this.authState.redirectUrl = state.url;

    return this.secure(route);
  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    // save snapshop url
    this.authState.redirectUrl = state.url;
    return this.canActivate(route, state);
  }

  canMatch(route: Route, segments: UrlSegment[]):  Observable<boolean> {
    // create the current route from segments
    const fullPath = segments.reduce((path, currentSegment) => {
      return `${path}/${currentSegment.path}`;
    }, '');

    this.authState.redirectUrl = fullPath;

    return this.secure(route);
  }

  private secure(route: ActivatedRouteSnapshot | Route): Observable<boolean> {
    // tap into auth state to see if user exists
    return this.authState.stateItem$.pipe(
      map((user) => {
        // if user exists let them in, else redirect to login
        if (!user) {
          this._router.navigateByUrl('/authentication/login');
          return false;
        }
        // user exists
        return true;
      })
    );
  }
}
