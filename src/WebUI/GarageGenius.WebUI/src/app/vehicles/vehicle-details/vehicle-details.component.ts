import { Component, OnInit } from '@angular/core';
import { VehiclesService as VehicleService } from '../service/vehicles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleResponseModel } from '../models/vehicle.model';
import { ReservationsService } from 'src/app/reservations/services/reservations.service';
import { VehicleReservationsResponseModel } from 'src/app/reservations/models/vehicle-reservations-response.model';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss'],
})
export class VehicleDetailsComponent implements OnInit {
  private readonly _vehicleService: VehicleService;
  private readonly _reservationsService: ReservationsService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _router: Router;
  public vehicleResponse?: VehicleResponseModel;
  public vehicleReservationsResponse?: Array<VehicleReservationsResponseModel>;

  constructor(
    vehicleService: VehicleService,
    reservationsService: ReservationsService,
    activatedRoute: ActivatedRoute,
    router: Router
  ) {
    this._vehicleService = vehicleService;
    this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
    this._router = router;
  }
  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const vehicleId = params['id'];
      this.vehicle(vehicleId);
    });
  }

  public vehicle(id: string) {
    return this._vehicleService.getVehicleById(id).subscribe((vehicle) => {
      this.vehicleResponse = vehicle;
    });
  }

  public goBack(): void {
    this._router.navigate(['dashboard/vehicles']);
  }

  public getVehicleReservations(vehicleId: string) {
    this._reservationsService
      .getVehicleReservations(vehicleId)
      .subscribe((reservations) => {
        this.vehicleReservationsResponse = reservations;
      });
  }
}
