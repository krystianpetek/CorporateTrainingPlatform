import { Injectable } from '@angular/core';
import { Observable, map, catchError, throwError, retry } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { HealthCheck } from './health-check/health-check.component';

export interface IHealthCheckService {
  healthCheckUsers(): Observable<HealthCheck>;
  healthCheckCustomers(): Observable<HealthCheck>;
  healthCheckVehicles(): Observable<HealthCheck>;
  healthCheckNotifications(): Observable<HealthCheck>;
}

@Injectable({
  providedIn: 'root',
})
export class HealthCheckService implements IHealthCheckService {
  private httpClient: HttpClient;
  private users = `health/users-module`;
  private customers = `health/customers-module`;
  private vehicles = `health/vehicles-module`;
  private notifications = `health/notifications-module`;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public healthCheckUsers(): Observable<HealthCheck> {
    return this.handleRequest(this.users);
  }
  public healthCheckCustomers(): Observable<HealthCheck> {
    return this.handleRequest(this.customers);
  }
  public healthCheckVehicles(): Observable<HealthCheck> {
    return this.handleRequest(this.vehicles);
  }
  public healthCheckNotifications(): Observable<HealthCheck> {
    return this.handleRequest(this.notifications);
  }

  private handleRequest(url: string): Observable<HealthCheck> {
    return this.httpClient.get<HealthCheck>(url).pipe(
      map((response) => response),
      retry(2),
      catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    switch (error.status) {
      case 0:
        console.error('Client error:', error.error);
        break;
      case HttpStatusCode.InternalServerError:
        console.error('Server error:', error.error);
        break;
      case HttpStatusCode.BadRequest:
        console.error('Request error:', error.error);
        break;
      default:
        console.error('Unknown error:', error.error);
        break;
    }
    return throwError(() => error);
  }
}
