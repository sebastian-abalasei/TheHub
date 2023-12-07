import {Component, OnInit} from '@angular/core';
import {AuthState} from "../api-authorization/auth-state";
import {map, Observable} from "rxjs";
import {LoggedUserInfo} from "../api-authorization/auth-models";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  status$: Observable<string>;
  constructor(private authState: AuthState) {}
  ngOnInit(): void {
    this.status$ = this.authState.stateItem$.pipe(
      map((state:LoggedUserInfo) => state?.email)
    );
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
