import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  public work?: string;

  constructor(http: HttpClient) {
    http.get<Response>('users-module/Account/health-check').subscribe(
      (result: Response) => {
        this.work = result.message;
      },
      (error) => console.error(error)
    );
  }

  title = 'GarageGenius.WebUI';
}

type Response = {
  message: string;
};
