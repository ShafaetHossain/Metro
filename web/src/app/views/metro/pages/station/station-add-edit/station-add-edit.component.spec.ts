import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StationAddEditComponent } from './station-add-edit.component';

describe('StationAddEditComponent', () => {
  let component: StationAddEditComponent;
  let fixture: ComponentFixture<StationAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StationAddEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StationAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
