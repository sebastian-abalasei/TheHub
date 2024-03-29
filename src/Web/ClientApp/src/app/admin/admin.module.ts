import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {DashboardComponent} from "./dashboard/dashboard.component";
import {QuizComponent} from "./edit-my-quizzes/quiz.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AccordionModule} from 'ngx-bootstrap/accordion';


@NgModule({
  declarations: [
    QuizComponent
  ],
  imports: [
    CommonModule,
    AccordionModule.forRoot(),
    RouterModule.forChild([
      {path: '', component: DashboardComponent},
      {path: 'dashboard', component: DashboardComponent},
      {path: 'edit-quiz', component: QuizComponent},
    ]),
    ReactiveFormsModule,
    FormsModule
  ]
})
export class AdminModule { }
