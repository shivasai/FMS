import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FeedbackQuestion} from '@app/_models/feedbackquestion'
import { FeedbackService } from '@app/_services/feedback.service';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-feedbackdetails',
  templateUrl: './feedbackdetails.component.html',
  styleUrls: ['./feedbackdetails.component.less']
})
export class FeedbackdetailsComponent implements OnInit {

  constructor(private fb: FormBuilder, private feedbackService : FeedbackService,private route: ActivatedRoute,private router: Router) { }
  submitted = false;
  questionId: number;
  feedbackQuestion : FeedbackQuestion = new FeedbackQuestion();
  answers = new Array();
  successtext="";
  ngOnInit(): void {    
    this.questionId = this.route.snapshot.queryParams['qId'];
    console.log("Question id " + this.questionId);
    this.feedbackService.GetFeedbackQuestion(this.questionId)
    .pipe(first())
            .subscribe(
                data => {
                  this.f.Question.setValue(data.question);
                  this.f.ParticipantType.setValue(data.participantType);
                  this.f.QuestionType.setValue(data.questionTye);
                  console.log(data.feedbackOptions);
                  for (let i = 0; i < data.feedbackOptions.length; i++) {   
                                    
                    this.answers.push( data.feedbackOptions[i].option);
                  }
                  console.log(this.answers);
                },
                error => {
                  
                });

  }
  questionForm = this.fb.group({
    QuestionType: ['MultipleAnswers'],
    ParticipantType : ['Participated'],
    Question:['',Validators.required]
  })
  get f() { return this.questionForm.controls; }

  onSubmit() {
   // console.log(this.answers);

    const clsAnswers = document.getElementsByClassName('clsAnswer');

   

    this.submitted=true;
    if (this.questionForm.invalid) {
      return;
    }
    //console.log(this.f.QuestionType.value);
    this.feedbackQuestion.Id = +this.questionId;
    this.feedbackQuestion.Question = this.f.Question.value;
    this.feedbackQuestion.ParticipantType = this.f.ParticipantType.value;
    this.feedbackQuestion.QuestionTye = this.f.QuestionType.value;
    this.feedbackQuestion.FeedbackOptions=[]
    for (let i = 0; i < clsAnswers.length; i++) {
      const ans = clsAnswers[i] as HTMLInputElement;
      console.log(ans.value)
      this.feedbackQuestion.FeedbackOptions.push(ans.value);
    }
    console.log(this.feedbackQuestion);
    this.feedbackService.SaveFeedbackQuestion(this.feedbackQuestion)
    .pipe(first())
            .subscribe(
                data => {
                   console.log("Success");
                   this.submitted=true;
                   this.successtext="Successfully inserted";
                   setTimeout(()=>{ 
                    this.successtext="";
                    this.router.navigate(['/feedback']);
                   }, 3000);
                },
                error => {
                  this.submitted=true;
                });
  }
  AddAnswer(){
    this.answers.push("");
  }
  DeleteQuestion(){
    this.feedbackService.DeleteFeedbackQuestion(this.questionId)
    .pipe(first())
            .subscribe(
                data => {
                   
                   this.submitted=true;
                   this.successtext="Deleted Successfully";
                   setTimeout(()=>{ 
                    this.successtext="";
                    this.router.navigate(['/feedback']);
                   }, 3000);
                },
                error => {
                  this.submitted=true;
                });
  }
  DeleteAnswer(id){
    console.log(id);
    this.answers.splice(id,1);
  }
  CancelQuestion(){
    this.router.navigate(['/feedback']);
  }
}
