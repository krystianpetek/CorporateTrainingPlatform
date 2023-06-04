import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HealthCheckComponent} from './health-check/health-check.component';
import {AppRoutingModule} from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {AuthenticationModule} from "./authentication/authentication.module";
import {JsonWebTokenInterceptor} from "./authentication/service/json-web-token.interceptor";

@NgModule({
  declarations: [AppComponent, HealthCheckComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    AuthenticationModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JsonWebTokenInterceptor, multi: true,},
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
export class AppModule {
}
