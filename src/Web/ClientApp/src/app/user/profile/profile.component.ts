import {Component} from '@angular/core';
import {AuthState} from "../../api-authorization/auth-state";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  constructor( private state:AuthState) {

  }

}
