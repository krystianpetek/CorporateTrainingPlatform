import { Injectable } from '@angular/core';
import { NotificationsHubModel } from './models/notifications-hub-model';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { Observable, Subject, from } from 'rxjs';
import { IStorageService } from '../storage/models/base-storage.service';
import { StorageService } from '../storage/storage.service';
import { MatSnackBar } from '@angular/material/snack-bar';

// TODO add signalr module ?? and  signalr routing ??

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private _hubConnection?: signalR.HubConnection;
  private _hubUrl: string;
  private _storageService: IStorageService;
  private messageSource: Subject<NotificationsHubModel> =
    new Subject<NotificationsHubModel>();
  public messageReceived$: Observable<NotificationsHubModel> =
    this.messageSource.asObservable();

  constructor(
    storageService: StorageService,
    private _matSnackBar: MatSnackBar
  ) {
    this._hubUrl = environment.notificationHubUrl;
    this._storageService = storageService;
    this.createHubConnection();
    this.establishHubConnection();
    this.registerHandlers();
  }

  public createHubConnection(): void {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this._hubUrl, {
        withCredentials: true,
        accessTokenFactory: () =>
          this._storageService.getKey(StorageService.JWT_KEY) as string,
      })
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();
  }
  public establishHubConnection(): void {
    from(this._hubConnection!.start()).subscribe(() => {
      console.log(
        `SignalR connection started!, connection id: ${this._hubConnection?.connectionId}`
      );
    });
  }

  public registerHandlers(): void {
    this._hubConnection?.on(
      `SendNotification`,
      (data: NotificationsHubModel) => {
        this.messageSource.next(data);
        console.log('event received');
      }
    );
  }

  public async stopHubConnection(): Promise<void> {
    await this._hubConnection?.stop();
    console.log(
      `SignalR connection terminated ${this._hubConnection?.connectionId}`
    );
    this._matSnackBar.open('unsubscribed', 'close', {
      duration: 2000,
      verticalPosition: 'top',
      horizontalPosition: 'right',
      direction: 'ltr',
      panelClass: ['green-snackbar', 'login-snackbar'],
      // ['mat-toolbar', 'mat-primary']
    });
  }

  //public isConnected: Observable<boolean> = new Observable<boolean>(() => {
  //  // TODO signalR connection state
  //  of(this._connection?.state == signalR.HubConnectionState.Connected)
  //    .pipe(
  //      timeout(5000),
  //      retry(5));
  //});

  //public startConnection = (): void => {
  //  this._connection = new signalR.HubConnectionBuilder()
  //    .withUrl(this._hubUrl, { withCredentials: false })
  //    .withAutomaticReconnect()
  //    .configureLogging(signalR.LogLevel.Information)
  //    .build();

  //  this._connection.start()
  //    .then(
  //      () => console.log(`SignalR connection started!, connection id: ${this._connection?.connectionId}`),
  //      (error) => console.log(`SignalR connection error: ${error}`)
  //    );

  //  this._connection.on(`SendMessage`, (data: NotificationsHubModel) => {
  //    this.data.push(data);
  //  });
  //  this._connection.on(`Package`, (data: NotificationsHubModel) => {
  //    this.data.push(data);
  //  });
  //}
}
