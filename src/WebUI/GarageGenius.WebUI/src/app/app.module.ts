import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationModule } from './authentication/authentication.module';
import { jwtInterceptorProvider } from './shared/json-web-token.interceptor';
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
    jwtInterceptorProvider
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
export class AppModule {}
