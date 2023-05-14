import { Component, OnInit } from '@angular/core';
import { HealthCheckService } from '../health-check.service';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.css'],
})
export class HealthCheckComponent {
  private healthCheckService: HealthCheckService;
  public moduleHealths: HealthCheck[] = [];

  constructor(healthCheckService: HealthCheckService) {
    this.healthCheckService = healthCheckService;
    this.healthCheckService
      .healthCheckUsers()
      .subscribe((response: HealthCheck) => {
        this.moduleHealths.push(response);
      });
    this.healthCheckService
      .healthCheckCars()
      .subscribe((response: HealthCheck) => {
        this.moduleHealths.push(response);
      });
    this.healthCheckService
      .healthCheckCustomers()
      .subscribe((response: HealthCheck) => {
        this.moduleHealths.push(response);
      });
    this.healthCheckService
      .healthCheckNotifications()
      .subscribe((response: HealthCheck) => {
        this.moduleHealths.push(response);
      });
  }
}
export type HealthCheck = {
  message: string;
};
