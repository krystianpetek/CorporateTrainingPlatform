import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-add',
  templateUrl: './reservation-add.component.html',
  styleUrls: ['./reservation-add.component.scss'],
})
export class ReservationAddComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _activatedRoute: ActivatedRoute;
  constructor(
    reservationsService: ReservationService,
    activatedRoute: ActivatedRoute
  ) {
    this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
  }

  public ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe((params) => {
      const vehicleId = params['vehicleId'];
      if (vehicleId) {
        console.log(vehicleId);
      }
    });
  }
}
