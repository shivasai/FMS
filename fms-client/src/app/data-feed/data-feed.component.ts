import { Component, OnInit } from '@angular/core';
import { DataFeedService } from '@app/_services/datafeed.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-data-feed',
  templateUrl: './data-feed.component.html',
  styleUrls: ['./data-feed.component.less']
})
export class DataFeedComponent implements OnInit {
  
  message: string;
  constructor(private datafeedService: DataFeedService) { }

  ngOnInit(): void {    
  }
  DataFeed(){
    this.datafeedService.DataFeed()
    .pipe(first())
    .subscribe(
        data => {
          this.message="Data Feed Successful!!!";
          setTimeout(()=>{ 
            this.message="";
           }, 3000);           
        },
        error => {
          this.message="An error occured";
          setTimeout(()=>{ 
            this.message="";
           }, 3000);
        });
  }
}
