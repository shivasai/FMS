import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { jqxGridModule } from 'jqwidgets-ng/jqxgrid';
import { jqxButtonModule } from 'jqwidgets-ng/jqxbuttons';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EventsComponent } from './events/events.component';
import { JqwgridComponent } from './jqwgrid/jqwgrid.component';
//import { CommonModule } from '@angular/common';
import { ConfigurationComponent } from './configuration';
import { FeedbackComponent } from './feedback/feedback.component'
;
import { FeedbackdetailsComponent } from './feedbackdetails/feedbackdetails.component';
import { ParticipantfeedbackComponent } from './participantfeedback/participantfeedback.component'
import { EventdetailsComponent } from './eventdetails/Eventdetails.component';;
import { ReportComponent } from './report/report.component'


@NgModule({
    imports: [
        BrowserModule,    
        //CommonModule,           
        ReactiveFormsModule,        
        HttpClientModule,
        AppRoutingModule,
        jqxGridModule,
        jqxButtonModule
        
    ],   
    declarations: [
        AppComponent,
        HomeComponent,
        AdminComponent,
        LoginComponent,
        DashboardComponent ,
        EventsComponent ,
        JqwgridComponent,
        ConfigurationComponent,
        FeedbackComponent ,
        FeedbackdetailsComponent ,
        ParticipantfeedbackComponent,                      
        EventdetailsComponent
, ReportComponent     ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }