export interface VehicleResponseModel {
  id: string;
  manufacturer: string;
  model: string;
  licensePlate: string;
  year: number;
  vin: string;
}

export interface VehicleRequestModel {
  manufacturer: string;
  model: string;
  licensePlate: string;
  year: number;
  vin: string;
}
