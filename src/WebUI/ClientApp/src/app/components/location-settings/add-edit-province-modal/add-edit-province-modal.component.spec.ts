import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditProvinceModalComponent } from './add-edit-province-modal.component';

describe('AddEditProvinceModalComponent', () => {
  let component: AddEditProvinceModalComponent;
  let fixture: ComponentFixture<AddEditProvinceModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditProvinceModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditProvinceModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
