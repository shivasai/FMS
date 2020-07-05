import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { FeedbackQuestion } from '@app/_models/feedbackquestion';
import { ParticipantFb } from '@app/_models/participantFb';


@Injectable({ providedIn: 'root' })
export class FeedbackService {
   
    constructor(        
        private http: HttpClient
    ) {
       
    }
    SaveFeedbackQuestion(postFeedback:FeedbackQuestion) {
        return this.http.post<any>(`${environment.apiUrl}/Feedback/`, postFeedback )
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    GetFeedbackQuestion(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/Feedback/` + id )
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    DeleteFeedbackQuestion(id:number) {
        return this.http.delete<any>(`${environment.apiUrl}/Feedback/` + id )
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    GetFeedbackByParticipant(postFeedback:FeedbackQuestion) {
        return this.http.post<any>(`${environment.apiUrl}/Feedback/FeedbacksByParticipants`, postFeedback )
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    SaveParticipantFeedback(participantFeedbacks:Array<ParticipantFb>) {
        return this.http.post<any>(`${environment.apiUrl}/Feedback/ParticipantFeedbacks`, participantFeedbacks )
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
}