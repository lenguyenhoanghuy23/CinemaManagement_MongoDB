import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowTimeComponent } from './show-time.component';

describe('ShowTimeComponent', () => {
  let component: ShowTimeComponent;
  let fixture: ComponentFixture<ShowTimeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShowTimeComponent]
    });
    fixture = TestBed.createComponent(ShowTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
