import { Component, OnInit, ViewChild } from '@angular/core';
import { DataFeedService } from '@app/_services/datafeed.service';

@Component({
  selector: 'app-data-feed',
  templateUrl: './data-feed.component.html',
  styleUrls: ['./data-feed.component.less']
})
export class DataFeedComponent implements OnInit {
  @ViewChild('fileInput') fileInput;
  message: string;
  constructor(private datafeedService: DataFeedService) { }

  ngOnInit(): void {
  }
  uploadFile() {
    let formData = new FormData();
    formData.append('upload', this.fileInput.nativeElement.files[0])

    this.datafeedService.UploadExcel(formData).subscribe(result => {
      this.message = result.toString();
      
    });
   

  }
}
