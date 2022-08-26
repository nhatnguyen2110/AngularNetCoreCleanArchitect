import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWeatherDataComponent } from './edit-weather-data.component';

describe('EditWeatherDataComponent', () => {
  let component: EditWeatherDataComponent;
  let fixture: ComponentFixture<EditWeatherDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditWeatherDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWeatherDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
