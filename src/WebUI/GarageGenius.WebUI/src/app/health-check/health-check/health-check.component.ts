import { Component, OnDestroy } from '@angular/core';
import { HealthCheckService } from '../services/health-check.service';
import { SignalrService } from '../../shared/services/signalr/signalr.service';
import { HealthCheckDisplayModel } from '../models/heallth-check-display.model';
import { ModulesModel } from '../models/modules.model';
import { HealthCheckResponseModel } from '../models/health-check-response.model';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.scss'],
})
export class HealthCheckComponent implements OnDestroy {
  private healthCheckService: HealthCheckService;
  public moduleHealths: Record<ModulesModel, HealthCheckDisplayModel> = {
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
      next: (response: HealthCheckResponseModel) => {
        this.moduleHealths.Vehicles.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Vehicles.module = {
          message: 'Vehicles module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckCustomers().subscribe({
      next: (response: HealthCheckResponseModel) => {
        this.moduleHealths.Customers.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Customers.module = {
          message: 'Customers module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckUsers().subscribe({
      next: (response: HealthCheckResponseModel) => {
        this.moduleHealths.Users.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Users.module = {
          message: 'Users module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckNotifications().subscribe({
      next: (response: HealthCheckResponseModel) => {
        this.moduleHealths.Notifications.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Notifications.module = {
          message: 'Notifications module is not available',
        };
      },
    });
    this.healthCheckService.healthCheckReservations().subscribe({
      next: (response: HealthCheckResponseModel) => {
        this.moduleHealths.Reservations.module = response;
      },
      error: (error: void) => {
        this.moduleHealths.Reservations.module = {
          message: 'Reservations module is not available',
        };
      },
    });
    signalrService.createHubConnection();
  }
  async ngOnDestroy(): Promise<void> {
    await this.signalrService.stopHubConnection();
  }
}

// https://www.typescriptlang.org/docs/handbook/utility-types.html#recordkeys-type
// https://blog.angular-university.io/rxjs-error-handling/
