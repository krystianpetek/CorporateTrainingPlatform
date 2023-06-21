import { Observable } from 'rxjs';
import { VehicleModel } from './vehicle.model';

export abstract class BaseVehicleService implements IVehiclesService {
  abstract getCustomerVehicles(
    customerId: string
  ): Observable<Array<VehicleModel>>;
  abstract getVehicleById(vehicleId: string): Observable<VehicleModel>;
}

export interface IVehiclesService {
  getCustomerVehicles(customerId: string): Observable<Array<VehicleModel>>;
  getVehicleById(vehicleId: string): Observable<VehicleModel>;
}
