import {ActivatedRoute, Router} from '@angular/router';

import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {catchError} from "rxjs/operators";
import {throwError} from "rxjs";
import {AuthState} from "../../api-authorization/auth-state";
import {ILoginRequest, InfoResponse, LoginRequest, UsersClient} from "../../web-api-client";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginFormControl = new FormGroup({
    email: new FormControl('administrator@localhost'),
    password: new FormControl('Administrator1!'),
  });
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];

  private _returnUrl: string | undefined;

  constructor(private _usersClient: UsersClient, private _router: Router, private _authState: AuthState, private _route: ActivatedRoute) {
  }

  ngOnInit(): void {

    this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  reloadPage(): void {
    window.location.reload();
  }

  public loginUser = () => {
    const userForAuth: ILoginRequest = {
      email: this.loginFormControl.get("email")?.value,
      password: this.loginFormControl.get("password")?.value
    }
    this._usersClient.postApiUsersLogin(true, false, new LoginRequest(userForAuth)).pipe(
      // it's better to pipe catchError
      catchError((error) => {
        console.log(error);
        return throwError(() => error);
      })
    )
      .subscribe({
        next: (result) => {
          this._usersClient.getApiUsersManageInfo().pipe(
            // redirect to dashbaord
            catchError((error) => {
              console.log(error);
              return throwError(() => error);
            })
          ).subscribe({
              next: (result: InfoResponse) => {
                this._authState.SaveSession(result);

                this._router.navigateByUrl(
                  this._authState.redirectUrl || '/user/dashboard'
                );
              }
            }
          )
        },
      })
  }
}
