import {KeyValue} from "@angular/common";

export interface UserForAuthenticationDto {
  email: string | null | undefined;
  password: string | null | undefined;
}

export interface UserForRegistrationDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface UserProfileModel {
  username: string;
  email: string;
  roles: string;
  claims: Array<KeyValue<string, string>>;
  userId: number;
}

export interface AuthResponseDto {
  username: string;
  email: string;
  roles: string;
  claims: Array<KeyValue<string, string>>;
  userId: number;
}
