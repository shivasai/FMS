import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '@app/_services/events.service';
import { first } from 'rxjs/operators';
import { Events } from '@app/_models/events';
import { EventPocDetails } from '@app/_models/eventpocdetails';
import { ParticipantFeedback } from '@app/_models/participantfeedback';
//import { CommonModule } from "@angular/common";
@Component({
  selector: 'app-eventdetails',
  templateUrl: './eventdetails.component.html',
  styleUrls: ['./eventdetails.component.less'],
  
})
export class EventdetailsComponent implements OnInit {
eventid: number;
eventDetails : Events;
eventPocDetails : Array<EventPocDetails>;
participantFeedbacks : Array<ParticipantFeedback>;
NotParticipatedFeedbacks : Array<ParticipantFeedback>;
UnregisteredFeedbacks : Array<ParticipantFeedback>;
  constructor(private route: ActivatedRoute,private eventService: EventService) { }

  ngOnInit(): void {
    
    this.eventid = this.route.snapshot.queryParams['eventid'];
    this.eventService.EventDetails(this.eventid)
    .pipe(first())
            .subscribe(
                data => {
                    this.eventDetails=data;
                },
                error => {
                    
                });
    

     this.getPocDetails();
     this.getFbDetails();
     this.getNotParticipatedFbDetails();
     this.getUnregisteredFbDetails();
  }
  getPocDetails(){
    this.eventService.EventPocDetails(this.eventid)
    .pipe(first())
            .subscribe(
                data => {
                    this.eventPocDetails = data;
                    //console.log(this.eventPocDetails);
                },
                error => {
                    
                });
  }

  getFbDetails(){
    this.eventService.ParticipantFeedbackDetails(this.eventid)
    .pipe(first())
            .subscribe(
                data => {
                  console.log(data);
                    this.participantFeedbacks = data;
                    console.log(this.participantFeedbacks);
                },
                error => {
                    
                });
  }
  getNotParticipatedFbDetails(){
    this.eventService.NotParticipatedFeedbackDetails(this.eventid)
    .pipe(first())
            .subscribe(
                data => {
                  console.log(data);
                    this.NotParticipatedFeedbacks = data;
                    console.log(this.NotParticipatedFeedbacks);
                },
                error => {
                    
                });
  }
  getUnregisteredFbDetails(){
    this.eventService.UnregisteredFeedbackDetails(this.eventid)
    .pipe(first())
            .subscribe(
                data => {
                  console.log(data);
                    this.UnregisteredFeedbacks = data;
                    console.log(this.UnregisteredFeedbacks);
                },
                error => {
                    
                });
  }
}


