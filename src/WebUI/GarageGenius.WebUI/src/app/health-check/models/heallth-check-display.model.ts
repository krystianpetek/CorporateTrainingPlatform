import { HealthCheckResponseModel } from './health-check-response.model';

export interface HealthCheckDisplayModel {
  module: HealthCheckResponseModel;
  name: string;
}
