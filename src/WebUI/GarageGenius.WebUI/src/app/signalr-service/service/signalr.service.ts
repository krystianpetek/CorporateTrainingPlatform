import { Injectable } from '@angular/core';
import { NotificationsHubModel } from '../notifications/notifications-hub-model';
import * as signalR from '@microsoft/signalr';
import { Observable, of, retry, Subject, timeout } from 'rxjs';

// TODO add signalr module ?? and  signalr routing ??

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private _hubUrl: string;
  private _connection?: signalR.HubConnection;
  public data: NotificationsHubModel[] = [];

  constructor() {
    this._hubUrl = '/notifications';
  }

  //public isConnected: Observable<boolean> = new Observable<boolean>(() => {
  //  // TODO signalR connection state
  //  of(this._connection?.state == signalR.HubConnectionState.Connected)
  //    .pipe(
  //      timeout(5000),
  //      retry(5));
  //});

  public async startConnection(): Promise<void> {
    try {
      this._connection = new signalR.HubConnectionBuilder()
        .withUrl(this._hubUrl, { withCredentials: false })
        .withAutomaticReconnect()
        .build();

      await this._connection.start();

      console.log(
        `SignalR connection started!, connection id: ${this._connection.connectionId}`
      );

      this._connection.on(`SendNotification`, (data: NotificationsHubModel) => {
        this.data.push(data);
      });

    } catch (error) {
      console.log(`SignalR connection error: ${error}`);
    }
  }

  public async stopConnection(): Promise<void> {
    await this._connection?.stop();
    console.log(`SignalR connection terminated ${this._connection?.connectionId}`);

  }

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
