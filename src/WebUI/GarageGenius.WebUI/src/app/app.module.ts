import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HealthCheckComponent } from './health-check/health-check.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [AppComponent, HealthCheckComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
  ],
  providers: [
    // TODO
    //{
    //  provide: APP_INITIALIZER,
    //  useFactory: async (signalrService: SignalrService) => {
    //    await signalrService.initializeStartConnection();
    //  },
    //  multi: true,
    //  deps: [SignalrService],
    //},
  ],
  bootstrap: [AppComponent],
  exports: [MatButtonModule],
})
export class AppModule { }
