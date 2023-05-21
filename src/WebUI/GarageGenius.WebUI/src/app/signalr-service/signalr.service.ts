import { Injectable } from '@angular/core';
import { NotificationsHubModel } from './notifications/notifications-hub-model';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private _hubUrl: string;
  private _httpClient: HttpClient;
  private _connection?: signalR.HubConnection;
  public data: NotificationsHubModel[] = [];

  constructor(httpClient: HttpClient) {
    this._hubUrl = '/notifications';
    this._httpClient = httpClient;
  }

  public async initializeStartConnection(): Promise<void> {
    try {
      this._connection = new signalR.HubConnectionBuilder()
        .withUrl(this._hubUrl)
        .withAutomaticReconnect()
        .build();

      await this._connection.start();

      console.log(
        `SignalR connection started!, connection id: ${this._connection.connectionId}`
      );
    } catch (error) {
      console.log(`SignalR connection error: ${error}`);
    }
  }
}
