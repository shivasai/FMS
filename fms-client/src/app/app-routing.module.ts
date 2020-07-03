import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EventsComponent } from './events/events.component';
import { ReportsComponent } from './reports/Reports.component';
import { EventdetailsComponent } from './eventdetails/Eventdetails.component';
import { JqwgridComponent } from './jqwgrid/jqwgrid.component';
import { AuthGuard } from './_helpers';
import { Role } from './_models';
import { ConfigurationComponent } from './configuration/configuration.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { FeedbackdetailsComponent } from './feedbackdetails/feedbackdetails.component';
import { ParticipantfeedbackComponent } from './participantfeedback/participantfeedback.component';

const routes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'login',
        component: LoginComponent
    },    
    {
        path: 'events',
        component: EventsComponent
    },
    {
        path: 'reports',
        component: ReportsComponent
    },
    {
        path: 'eventdetails',
        component: EventdetailsComponent
    },
    {
        path: 'jqwgrid',
        component: JqwgridComponent
    },
    {
        path: 'configuration',
        component: ConfigurationComponent
    },
    {
        path: 'feedback',
        component: FeedbackComponent
    },
    {
        path: 'feedbackdetails',
        component: FeedbackdetailsComponent
    },
    {
        path: 'participantfeedback',
        component: ParticipantfeedbackComponent
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
