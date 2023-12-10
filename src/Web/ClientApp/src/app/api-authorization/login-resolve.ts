import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot,} from '@angular/router';
import {map, Observable} from 'rxjs';
import {AuthState} from "./auth-state";
import {InfoResponse} from "../web-api-client";

@Injectable({providedIn: 'root'})
export class LoginResolve implements Resolve<boolean> {
  constructor(private authState: AuthState, private router: Router) {
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.authState.stateItem$.pipe(
      map((user: InfoResponse) => {
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
