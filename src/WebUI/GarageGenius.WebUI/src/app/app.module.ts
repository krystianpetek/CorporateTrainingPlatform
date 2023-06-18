import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationModule } from './authentication/authentication.module';
import { jwtInterceptorProvider } from './shared/interceptors/json-web-token.interceptor';
import { AppMaterialModule } from './shared/app-material.module';
import { ErrorComponent } from './shared/components/error/error.component';
import { HomeModule } from './home/home.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { SnackBarMessageModule } from './shared/services/snack-bar-message/snack-bar-message.module';
import { UsersModule } from './users/users.module';
import { UsersRoutingModule } from './users/users-routing.module';
import { VehiclesComponent } from './vehicles/vehicles.component';

@NgModule({
  declarations: [AppComponent, ErrorComponent, VehiclesComponent],
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
