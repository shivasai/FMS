import { Component, OnInit } from '@angular/core';
import { DashboardService } from '@app/_services/dashboard.service';
import { first } from 'rxjs/operators';
import { Dashboard } from '@app/_models/dashboard';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less']
})
export class DashboardComponent implements OnInit {

  constructor(private dashboardService: DashboardService) { }
  dashboard : Dashboard;
  ngOnInit(): void {
    this.dashboardService.dashboardData()
    .pipe(first())
            .subscribe(
                data => {
                    this.dashboard = data;
                },
                error => {
                    
                });
  }

}
