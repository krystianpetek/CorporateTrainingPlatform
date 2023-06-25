import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HealthCheckComponent } from './health-check/health-check.component';
import { HealthCheckRoutingModule } from './health-check-routing.module';

@NgModule({
  declarations: [HealthCheckComponent],
  imports: [CommonModule, HealthCheckRoutingModule],
})
export class HealthCheckModule {}
