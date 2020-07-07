import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataFeedService {

  constructor(private http: HttpClient) { }

  DataFeed(){
    return this.http.put<any>(`${environment.apiUrl}/events/DataFeed/`,{})
            .pipe(map(data => {                            
                return data;
            }));
  }
 
}
