import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingReservationListComponent } from './pending-reservation-list.component';

describe('PendingReservationsComponent', () => {
  let component: PendingReservationListComponent;
  let fixture: ComponentFixture<PendingReservationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PendingReservationListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PendingReservationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
