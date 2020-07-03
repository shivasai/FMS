import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JqwgridComponent } from './jqwgrid.component';

describe('JqwgridComponent', () => {
  let component: JqwgridComponent;
  let fixture: ComponentFixture<JqwgridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JqwgridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JqwgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
