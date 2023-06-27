import { Component, OnInit } from '@angular/core';
import { VehiclesService as VehicleService } from '../service/vehicles.service';
import { ActivatedRoute } from '@angular/router';
import { VehicleResponseModel } from '../models/vehicle.model';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss'],
})
export class VehicleDetailsComponent implements OnInit {
  private readonly _vehicleService: VehicleService;
  private readonly _router: ActivatedRoute;
  public vehicleResponse?: VehicleResponseModel;

  constructor(vehicleService: VehicleService, router: ActivatedRoute) {
    this._vehicleService = vehicleService;
    this._router = router;
  }
  ngOnInit(): void {
    this._router.params.subscribe((params) => {
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
    window.history.back();
    // TODO - change a back button handling
  }
}
