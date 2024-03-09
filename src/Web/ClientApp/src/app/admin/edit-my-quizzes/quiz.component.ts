import {Component, OnInit, TemplateRef} from '@angular/core';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {
  CreateQuizCommand,
  CreateTodoListCommand, ICreateQuizCommand, QuizAggregate
} from '../../web-api-client';
import {IdValueDto, QuizzesClient} from "../../web-api-client";

@Component({
  selector: 'app-quiz-component',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.scss']
})
export class QuizComponent implements OnInit {
  debug: boolean = false;
  quizzes: IdValueDto[] = [];
  selectedQuiz: QuizAggregate = null;
  newQuizModalRef: BsModalRef;
  newQuizEditor: any = {};


  constructor(
    private quizClient: QuizzesClient,
    private modalService: BsModalService
  ) {
  }

  ngOnInit() {
    this.quizClient.getQuizzes().subscribe({
        next: result => {
          this.quizzes = result;
        },
        error: error => console.error(error)
      }
    );
  }

  // Quizzes
  newQuizCancelled(): void {
    this.newQuizModalRef.hide();
    this.newQuizEditor = {};
  }

  showNewQuizModal(template: TemplateRef<any>): void {
    this.newQuizModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById('title').focus(), 250);
  }

  selectQuiz(quiz: IdValueDto): void {
    this.quizClient.getQuiz(quiz.id).subscribe(
      {
        next: result => {
          this.selectedQuiz= result;
        },
        error: error => console.error(error)
      }
    );
  }

  deleteQuestion(): void{

  }
  addList(): void {
    const quizTitle: string = this.newQuizEditor.title;
    let x = new CreateQuizCommand({title: quizTitle});
    this.selectedQuiz = new IdValueDto();
    this.selectedQuiz.title = quizTitle;
    this.quizClient.createQuiz(x as CreateQuizCommand).subscribe(
      {
        next: result => {
          this.selectedQuiz.id = result;
        },
        error: error => console.error(error)
      }
    );
    this.quizzes.push(this.selectedQuiz);
    this.newQuizModalRef.hide();
    this.newQuizEditor = {};

  }

}
