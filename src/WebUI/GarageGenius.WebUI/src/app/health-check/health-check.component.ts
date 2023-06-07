import { Component, OnDestroy } from '@angular/core';
import { HealthCheckService } from './health-check.service';
import { SignalrService } from '../signalr-service/service/signalr.service';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.css'],
})
export class HealthCheckComponent implements OnDestroy {
  private healthCheckService: HealthCheckService;
  public moduleHealths: Record<Modules, HealthCheckDisplay> = {
    Vehicles: {
      module: { message: 'Loading...' },
      name: 'Vehicles',
    },
    Customers: {
      module: { message: 'Loading...' },
      name: 'Customers',
    },
    Notifications: {
      module: { message: 'Loading...' },
      name: 'Notifications',
    },
    Users: {
      module: { message: 'Loading...' },
      name: 'Users',
    },
    Reservations: {
      module: { message: 'Loading...' },
      name: 'Reservations',
    },
  };
  public signalrService: SignalrService;

  constructor(
    healthCheckService: HealthCheckService,
    signalrService: SignalrService
  ) {
    this.signalrService = signalrService;
    this.healthCheckService = healthCheckService;
    this.healthCheckService.healthCheckVehicles().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Vehicles.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Vehicles.module = {
          message: 'Vehicles module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckCustomers().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Customers.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Customers.module = {
          message: 'Customers module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckUsers().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Users.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Users.module = {
          message: 'Users module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckNotifications().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Notifications.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Notifications.module = {
          message: 'Notifications module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckReservations().subscribe({
      next: (response: HealthCheck) => {
        this.moduleHealths.Reservations.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Reservations.module = {
          message: 'Reservations module is not available',
        };
      },
    });
    signalrService.startConnection();
  }
  ngOnDestroy(): void {
    this.signalrService.stopConnection();
  }
}
export interface HealthCheck {
  message: string;
}

export interface HealthCheckDisplay {
  module: HealthCheck;
  name: string;
}

export type Modules = 'Users' | 'Vehicles' | 'Customers' | 'Notifications' | 'Reservations';
// https://www.typescriptlang.org/docs/handbook/utility-types.html#recordkeys-type
// https://blog.angular-university.io/rxjs-error-handling/
