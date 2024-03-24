import {Component, OnInit, TemplateRef} from '@angular/core';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';

import {
  Answer,
  CreateQuizCommand,
  CreateTodoListCommand, ICreateQuizCommand, Question, QuizAggregate
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
  oneAtATime = true;
  selectedQuestion: Question;


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
          this.selectedQuiz = result;
          this.selectedQuestion = this.selectedQuiz.questions[0];
          this.selectedQuestion.isEditable = true;
        },
        error: error => console.error(error)
      }
    );
  }

  deleteQuestion(): void {

  }

  addList(): void {
    const quizTitle: string = this.newQuizEditor.title;
    let createQuizCommand: CreateQuizCommand = new CreateQuizCommand({title: quizTitle});
    this.selectedQuiz = new IdValueDto();
    this.selectedQuiz.title = quizTitle;
    this.quizClient.createQuiz(createQuizCommand as CreateQuizCommand).subscribe(
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

  selectQuestion(question: Question): void {
    this.selectedQuiz.questions.forEach((question: Question): void => {
      question.isEditable = false;
    });
    this.selectedQuestion = question
    this.selectedQuestion.isEditable = true;
  }

  addNewQuestion() {

    this.selectedQuestion = new Question({text: "", quizId: this.selectedQuiz.id, answers: new Array<Answer>()});

    this.selectedQuiz.questions.push(this.selectedQuestion);
    this.selectQuestion(this.selectedQuestion);
  }

  onAnswerKeyUp($event: KeyboardEvent) {
    const inputValue: string = ($event.target as HTMLInputElement).value;
    if (inputValue !== "" && inputValue.length > 0 && this.selectedQuestion.answers.length < 4) {
      this.addEmptyAnswer();
    }
  }

  private addEmptyAnswer() {
    let hasOneEmpty: boolean = this.selectedQuestion.answers.some((str) => {
      return str.text.length === 0;
    });
    if (!hasOneEmpty) {
      this.selectedQuestion.answers.push(new Answer({text: "", isCorrect: false}));
    }
  }

  onQuestionKeyUp($event: KeyboardEvent) {
    const inputValue: string = ($event.target as HTMLInputElement).value;
    if (inputValue !== "" && inputValue.length > 0 && this.selectedQuestion.answers.length < 4) {
      this.addEmptyAnswer();
    }
  }

  saveQuiz() {
  }
}
