import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingReservationDetailsComponent } from './pending-reservation-details.component';

describe('PendingReservationDetailsComponent', () => {
  let component: PendingReservationDetailsComponent;
  let fixture: ComponentFixture<PendingReservationDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PendingReservationDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PendingReservationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
