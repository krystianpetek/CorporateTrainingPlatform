import { Component, OnInit } from '@angular/core';
import { ReservationsService } from '../services/reservations.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-add',
  templateUrl: './reservation-add.component.html',
  styleUrls: ['./reservation-add.component.scss'],
})
export class ReservationAddComponent implements OnInit {
  private readonly _reservationsService: ReservationsService;
  private readonly _activatedRoute: ActivatedRoute;
  constructor(
    reservationsService: ReservationsService,
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
