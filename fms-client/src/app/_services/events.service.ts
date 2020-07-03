import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';


@Injectable({ providedIn: 'root' })
export class EventService {
   
    constructor(        
        private http: HttpClient
    ) {
       
    }
    EventDetails(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/events/`+id)
            .pipe(map(eventdetails => {                              
                return eventdetails;
            }));
    }

    EventPocDetails(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/events/EventPocDetails/`+id)
            .pipe(map(eventpocdetails => {                              
                return eventpocdetails;
            }));
    }
    ParticipantFeedbackDetails(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/Feedback/ParticipantFeedbacks/`+id)
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    NotParticipatedFeedbackDetails(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/Feedback/NotParticipatedFeedbacks/`+id)
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
    UnregisteredFeedbackDetails(id:number) {
        return this.http.get<any>(`${environment.apiUrl}/Feedback/UnregisteredFeedbacks/`+id)
            .pipe(map(fbdetails => {                              
                return fbdetails;
            }));
    }
}