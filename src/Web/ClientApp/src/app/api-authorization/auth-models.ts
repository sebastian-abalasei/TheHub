import {IInfoResponse, InfoResponse} from "../web-api-client";

export class LoggedUserInfo extends InfoResponse implements IInfoResponse {
  expiresAt?: number;
}
