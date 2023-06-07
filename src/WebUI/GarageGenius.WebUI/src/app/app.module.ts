import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationModule } from "./authentication/authentication.module";
import { JsonWebTokenInterceptor } from "./authentication/service/json-web-token.interceptor";
import { AppMaterialModule } from './app-material.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AuthenticationModule,
    AppMaterialModule,
    AppRoutingModule,
  ],
  exports: [AppMaterialModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JsonWebTokenInterceptor, multi: true, },
    //{
    //  //TODO
    //  provide: APP_INITIALIZER,
    //  useFactory: async (signalrService: SignalrService) => {
    //    await signalrService.startConnection();
    //  },
    //  multi: true,
    //  deps: [SignalrService],
    //},
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
