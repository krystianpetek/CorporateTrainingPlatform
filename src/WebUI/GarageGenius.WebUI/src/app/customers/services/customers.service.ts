import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {environment} from "../../../environments/environment";
import {GetCustomerResponseModel} from "../models/get-customer-response-model";
import {UpdateCustomerRequestModel} from "../models/update-customer-request-model";

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  constructor(private _httpClient: HttpClient) {
  }

  getCustomer(customerId: string): Observable<GetCustomerResponseModel> {
    return this._httpClient
      .get<GetCustomerResponseModel>(environment.getCustomerUrl + `${customerId}`)
      .pipe(catchError(this.handleError))
  }

  updateCustomer(customer: UpdateCustomerRequestModel): Observable<any>{
    return this._httpClient
      .put<UpdateCustomerRequestModel>(environment.updateCustomerUrl, customer)
      .pipe(catchError(this.handleError))
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    // TODO - add a remote logging service like in backend - Serilog with Seq sink
    return throwError(() => errorMessage);
  }

}
