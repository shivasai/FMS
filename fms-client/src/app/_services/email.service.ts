import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';


@Injectable({ providedIn: 'root' })
export class EmailService {
   
    constructor(        
        private http: HttpClient
    ) {
       
    }
    SendEmail(eventId:string) {
        console.log("service")
        return this.http.post<any>(`${environment.emailServiceUrl}/MailService`, { eventId })
            .pipe(map(emaildetails => {                              
                return emaildetails;
            }));
    }
    SendReport(email:string) {
        console.log("service")
        return this.http.post<any>(`${environment.emailServiceUrl}/MailService/SendReport`, { email })
            .pipe(map(reportdetails => {                              
                return reportdetails;
            }));
    }

}