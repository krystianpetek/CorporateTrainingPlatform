import { Component, OnInit } from '@angular/core';
import { VehiclesService as VehicleService } from '../service/vehicles.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss'],
})
export class VehicleDetailsComponent implements OnInit {
  private readonly _vehicleService: VehicleService;
  private readonly _router: ActivatedRoute;
  constructor(vehicleService: VehicleService, router: ActivatedRoute) {
    this._vehicleService = vehicleService;
    this._router = router;
  }
  ngOnInit(): void {
    this._router.params.subscribe((params) => {
      this.vehicle(params['id']);
    });
  }

  public vehicle(id: string) {
    return this._vehicleService.getVehicleById(id).subscribe((vehicle) => {
      console.log(vehicle);
    });
  }
}
