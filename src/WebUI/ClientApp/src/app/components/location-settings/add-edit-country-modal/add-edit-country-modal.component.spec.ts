import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditCountryModalComponent } from './add-edit-country-modal.component';

describe('AddEditCountryModalComponent', () => {
  let component: AddEditCountryModalComponent;
  let fixture: ComponentFixture<AddEditCountryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditCountryModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditCountryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
