﻿import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {BehaviorSubject, Observable, timeout} from 'rxjs';
import {LoggedUserInfo} from "./auth-models";

@Injectable({ providedIn: 'root' })
export class AuthState {
  private timeout:number =60000;
  // create an internal subject and an observable to keep track
  private stateItem: BehaviorSubject<LoggedUserInfo | null> = new BehaviorSubject(
    null
  );
  stateItem$: Observable<LoggedUserInfo | null> = this.stateItem.asObservable();

  // redirect update
  get redirectUrl(): string {
    return localStorage.getItem('redirectUrl');
  }
  set redirectUrl(value: string) {
    localStorage.setItem('redirectUrl', value);
  }

  constructor(private router: Router){
    // simpler to initiate state here
    // check item validity
    const _localuser: LoggedUserInfo = this._GetUser();

    if (this.CheckAuth(_localuser)) {
      this.SetState(_localuser);
    } else {
      this.Logout(true);
    }
  }
  // shall move soon to state service
  SetState(item: LoggedUserInfo) {
    this.stateItem.next(item);
    return this.stateItem$;
  }
  UpdateState(item: Partial<LoggedUserInfo>) {
    const newItem = { ...this.stateItem.getValue(), ...item };
    // @ts-ignore
    this.stateItem.next(newItem);
    return this.stateItem$;
  }
  RemoveState() {
    this.stateItem.next(null);
  }

  // localstorage related methods
  private _SaveUser(user: LoggedUserInfo) {
    localStorage.setItem('user', JSON.stringify(user));
  }
  private _RemoveUser() {
    localStorage.removeItem('user');
  }

  private _GetUser(): LoggedUserInfo | null {
    const _localuser: LoggedUserInfo = JSON.parse(localStorage.getItem('user'));
    if (_localuser && _localuser.email) {
      return <LoggedUserInfo>_localuser;
    }
    return null;
  }

  // adding cookie saving methods


  // new saveSessions method
  SaveSession(user: LoggedUserInfo): LoggedUserInfo | null {
    if (user?.email) {
      user.expiresAt = Date.now() + this.timeout;
      this._SaveUser(user);
      this.SetState(user);
      return user;
    } else {
      // remove token from user
      this._RemoveUser();
      this.RemoveState();
      return null;
    }
  }

  UpdateSession(user: LoggedUserInfo) {
    const _localuser: LoggedUserInfo = this._GetUser();
    if (_localuser) {
      // only set accesstoken and refreshtoken
      _localuser.email = user.email;
      // _localuser.claims = user.claims;
      user.expiresAt = Date.now()+this.timeout;

      this._SaveUser(_localuser);
      this.UpdateState(user);
    } else {
      // remove token from user
      this._RemoveUser();
      this.RemoveState();
    }
  }

  CheckAuth(user: LoggedUserInfo) {
    // if no user, or no accessToken, something terrible must have happened
    if(user!=null && user!=undefined && user.email!=undefined){
      return !(user.expiresAt < Date.now())
    }
    return false;

  }

  // reroute optionally
  Logout(reroute: boolean = false) {
    // remove leftover
    this.RemoveState();
    // and clean localstroage
    this._RemoveUser();

    if (reroute) {
      this.router.navigateByUrl('/authentication/login');
    }
  }

}
