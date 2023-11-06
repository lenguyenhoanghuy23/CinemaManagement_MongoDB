import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreenTypeComponent } from './screen-type.component';

describe('ScreenTypeComponent', () => {
  let component: ScreenTypeComponent;
  let fixture: ComponentFixture<ScreenTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScreenTypeComponent]
    });
    fixture = TestBed.createComponent(ScreenTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
