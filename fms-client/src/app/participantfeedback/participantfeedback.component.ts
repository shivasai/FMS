import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '@app/_services/feedback.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { FeedbackQuestion } from '@app/_models/feedbackquestion';
import { ParticipantFb } from '@app/_models/participantFb';
@Component({
  selector: 'app-participantfeedback',
  templateUrl: './participantfeedback.component.html',
  styleUrls: ['./participantfeedback.component.less']
})
export class ParticipantfeedbackComponent implements OnInit {
  eventid: number;
  participantType: string; 
  email : string;
  postFeedback : FeedbackQuestion = new FeedbackQuestion();
  questionData:any[];
  participantFb = new Array<ParticipantFb>();
  constructor(private feedbackService: FeedbackService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.eventid = this.route.snapshot.queryParams['eventid'];
    this.participantType = this.route.snapshot.queryParams['type'];
    this.email = this.route.snapshot.queryParams['email'];
    if(this.participantType == "participated"){
      this.participantType = "Participated";
    }
    else if(this.participantType == "notparticipated"){
      this.participantType = "NotParticipated";
    }
    else if(this.participantType == "unregistered"){
      this.participantType = "Unregistered";
    }
   this.postFeedback.ParticipantType = this.participantType;
    this.feedbackService.GetFeedbackByParticipant(this.postFeedback)
    .pipe(first())
            .subscribe(
                data => {
                  this.questionData = data;
                  //console.log(data);
                  this.LoadRequestData(data);
                },
                error => {
                  
                });

  }
  LoadRequestData(inputData: any){
    for(let singleData of inputData){
      const pfb = new ParticipantFb();
      pfb.EventId = +this.eventid;
      pfb.Email = this.email;
      pfb.ParticipantType = this.participantType;
      pfb.QuestionId = singleData.id;
      pfb.QuestionType = singleData.questionTye;
      this.participantFb.push(pfb);
    }
    //console.log(this.participantFb)
  }
  SubmitFeedback(){
    for(let singleData of this.participantFb){
      //console.log(singleData);
      if(singleData.QuestionType=="MultipleAnswers"){
        console.log(singleData.QuestionId)
        const quesVal = (<HTMLOptionElement>document.querySelector('input[name="'+singleData.QuestionId+'"]:checked')).value;
       // console.log(quesVal)
        singleData.Answer = quesVal;
      }
      if(singleData.QuestionType=="FreeTextAnswer"){
        console.log(singleData.QuestionId)
        const quesVal = (<HTMLInputElement>document.getElementById(singleData.QuestionId)).value;
      //  console.log(quesVal)
        if(quesVal==""){
          alert("Please fill all fields");
          return;
        }
        singleData.Answer = quesVal;
      }
      
    }    
    console.log(this.participantFb);
    this.feedbackService.SaveParticipantFeedback(this.participantFb)
    .pipe(first())
            .subscribe(
                data => {
                  this.router.navigate(['/thankyou']);
                },
                error => {
                  alert(error)
                });
  }
}
