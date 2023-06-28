import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleReservationsComponent } from './vehicle-reservations.component';

describe('VehicleReservationsComponent', () => {
  let component: VehicleReservationsComponent;
  let fixture: ComponentFixture<VehicleReservationsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VehicleReservationsComponent]
    });
    fixture = TestBed.createComponent(VehicleReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
