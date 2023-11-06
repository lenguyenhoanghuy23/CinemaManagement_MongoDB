import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountrieComponent } from './countrie.component';

describe('CountrieComponent', () => {
  let component: CountrieComponent;
  let fixture: ComponentFixture<CountrieComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CountrieComponent]
    });
    fixture = TestBed.createComponent(CountrieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
