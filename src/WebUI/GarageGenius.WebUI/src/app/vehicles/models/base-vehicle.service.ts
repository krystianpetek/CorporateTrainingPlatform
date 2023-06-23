import { Observable } from 'rxjs';
import { VehicleRequestModel, VehicleResponseModel } from './vehicle.model';
import { SearchVehicleVinLicenseModel } from './search-vehicle-vin-license.model';

export abstract class BaseVehicleService implements IVehiclesService {
  abstract getCustomerVehicles(
    customerId: string
  ): Observable<Array<VehicleResponseModel>>;

  abstract getVehicleById(vehicleId: string): Observable<VehicleResponseModel>;

  abstract searchVehicleByVinAndLicensePlate(
    searchVehicle: SearchVehicleVinLicenseModel
  ): Observable<VehicleResponseModel>;

  abstract postVehicleForCustomer(
    customerId: string,
    vehicle: VehicleRequestModel
  ): Observable<VehicleRequestModel>;

  abstract updateVehicleCustomer(
    vehicleId: string,
    customerId: string
  ): Observable<void>;
}

export interface IVehiclesService {
  getCustomerVehicles(
    customerId: string
  ): Observable<Array<VehicleResponseModel>>;

  getVehicleById(vehicleId: string): Observable<VehicleResponseModel>;

  searchVehicleByVinAndLicensePlate(
    searchVehicle: SearchVehicleVinLicenseModel
  ): Observable<VehicleResponseModel>;

  postVehicleForCustomer(
    customerId: string,
    vehicle: VehicleRequestModel
  ): Observable<VehicleRequestModel>;

  updateVehicleCustomer(
    vehicleId: string,
    customerId: string
  ): Observable<void>;
}
