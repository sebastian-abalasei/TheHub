import { Injectable } from '@angular/core';
import {
  Resolve,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable, map } from 'rxjs';
import {AuthState} from "./auth-state";
import {UserInfo} from "../web-api-client";

@Injectable({ providedIn: 'root' })
export class LoginResolve implements Resolve<boolean> {
  constructor(private authState: AuthState, private router: Router) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.authState.stateItem$.pipe(
      map((user : UserInfo) => {
        // if logged in succesfully, go to last url
        if (user) {
          this.router.navigateByUrl(
            this.authState.redirectUrl || '/user/dashboard'
          );
        }
        // does not really matter, I either go in or navigate away
        return true;
      })
    );
  }
}
