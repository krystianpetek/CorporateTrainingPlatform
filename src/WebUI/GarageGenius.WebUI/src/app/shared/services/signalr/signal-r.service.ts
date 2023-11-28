import {Injectable, OnDestroy} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { Observable, Subject, from } from 'rxjs';
import { IStorageService } from '../storage/models/base-storage.service';
import { StorageService } from '../storage/storage.service';
import { SnackBarMessageService } from '../snack-bar-message/snack-bar-message.service';

// TODO add signalr module ?? and  signalr routing ??

@Injectable({
  providedIn: 'root',
})
export class SignalRService implements OnDestroy {
  private _hubConnection?: signalR.HubConnection;
  private _hubUrl: string;

  private _storageService: IStorageService;
  private _snackBarService: SnackBarMessageService;
  private messageSource: Subject<string> = new Subject<string>();
  public messageReceived$: Observable<string> =
    this.messageSource.asObservable();

  constructor(
    storageService: StorageService,
    snackBarService: SnackBarMessageService
  ) {
    this._hubUrl = environment.notificationHubUrl;
    this._storageService = storageService;
    this._snackBarService = snackBarService;
  }

  ngOnDestroy(): void {
    this.stopHubConnection().then(r => console.log(r));
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

      from(this._hubConnection!.start())
        .subscribe(() => {
          console.log(`SignalR connection started!, connection id: ${this._hubConnection?.connectionId}`);
          console.log(this._hubConnection?.state);
        });
  }

  public registerHandlers(): void {
    this._hubConnection?.on(`SendNotification`, (date, email) => {
      this.messageSource.next(email);
      this._snackBarService.success(email, 5);
    });
  }

  public async invoke(): Promise<void> {
    await this._hubConnection?.invoke(`SendMessage `, 'test,', 'test');
  }

  public async stopHubConnection(): Promise<void> {
    // this.messageSource.unsubscribe();
    await this._hubConnection?.stop();
    console.log(`SignalR connection terminated ${this._hubConnection?.connectionId}`);
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
