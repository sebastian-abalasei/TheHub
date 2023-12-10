import {Router} from '@angular/router';
import {
  PasswordConfirmationValidatorService
} from '../../shared/custom-validators/password-confirmation-validator.service';

import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

import {IRegisterRequest, UsersClient} from "../../web-api-client";

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  registerForm = new FormGroup({
    firstName: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required]),
    confirm: new FormControl<string>('', [Validators.required, this._passConfValidator.validateConfirmPassword])
  });

  public errorMessage = '';
  public showError?: boolean;

  constructor(private _usersClient: UsersClient, private _passConfValidator: PasswordConfirmationValidatorService,
              private _router: Router) {
  }

  ngOnInit() {
    this.registerForm = new FormGroup({
      firstName: new FormControl<string>(''),
      lastName: new FormControl<string>(''),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [Validators.required]),
      confirm: new FormControl<string>('', [Validators.required, this._passConfValidator.validateConfirmPassword])
    });
  }

  public registerUser = () => {
    this.showError = false;
    const userForReg: IRegisterRequest = {
      email: this.registerForm.get("email")?.value,
      password: this.registerForm.get("password")?.value
    };

    this._router.navigate(['/user']);
    // this._usersClient.postApiUsersRegister(new RegisterRequest(userForReg)).subscribe(opt =>{
    // });

  }
}
