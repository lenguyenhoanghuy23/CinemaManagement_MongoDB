import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CinemaZoomComponent } from './cinema-zoom.component';

describe('CinemaZoomComponent', () => {
  let component: CinemaZoomComponent;
  let fixture: ComponentFixture<CinemaZoomComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CinemaZoomComponent]
    });
    fixture = TestBed.createComponent(CinemaZoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
