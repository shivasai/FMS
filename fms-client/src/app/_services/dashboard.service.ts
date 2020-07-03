import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';


@Injectable({ providedIn: 'root' })
export class DashboardService {
   
    constructor(        
        private http: HttpClient
    ) {
       
    }
    dashboardData() {
        return this.http.get<any>(`${environment.apiUrl}/dashboard/`)
            .pipe(map(dashboard => {              
                console.log(dashboard);
                return dashboard;
            }));
    }

}