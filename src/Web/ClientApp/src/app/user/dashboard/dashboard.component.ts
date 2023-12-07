import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  statements = new Array<string>();

  constructor(private _router: Router, private _route: ActivatedRoute) {
  }

  ngOnInit(): void {
  }

}
