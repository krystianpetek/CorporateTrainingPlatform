import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationModule } from './authentication/authentication.module';
import { jwtInterceptorProvider } from './shared/interceptors/authorization.interceptor';
import { AppMaterialModule } from './shared/app-material.module';
import { ErrorComponent } from './shared/components/error/error.component';
import { HomeModule } from './home/home.module';
import { SnackBarMessageModule } from './shared/services/snack-bar-message/snack-bar-message.module';

@NgModule({
  declarations: [AppComponent, ErrorComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppMaterialModule,
    SnackBarMessageModule,
    AuthenticationModule,
    HomeModule,
  ],
  exports: [AppMaterialModule],
  providers: [
    jwtInterceptorProvider,
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
