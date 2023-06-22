import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from '../core/main/main.component';
import { AuthenticationModule } from '../authentication/authentication.module';
import { jwtInterceptorProvider } from '../shared/interceptors/authorization.interceptor';
import { AppMaterialModule } from '../shared/app-material.module';
import { ErrorComponent } from '../shared/components/error/error.component';
import { HomeModule } from '../home/home.module';
import { SnackBarMessageModule } from '../shared/services/snack-bar-message/snack-bar-message.module';
import { LayoutComponent } from '../core/layout/layout.component';
import { HeaderComponent } from '../core/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,
    LayoutComponent,
    HeaderComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
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
