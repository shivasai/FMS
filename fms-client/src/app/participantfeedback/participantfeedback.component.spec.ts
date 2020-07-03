import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParticipantfeedbackComponent } from './participantfeedback.component';

describe('ParticipantfeedbackComponent', () => {
  let component: ParticipantfeedbackComponent;
  let fixture: ComponentFixture<ParticipantfeedbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParticipantfeedbackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParticipantfeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
