import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { catchError, Observable, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private httpClient: HttpClient,
  ) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  get<T>(url: string, endpoint: string): Observable<T> {
    return this.httpClient
      .get<T>(`${url}/${endpoint}`)
      .pipe(retry(0), catchError(this.handleError));
  }

  post<T>(url: string, endpoint: string, item: any): Observable<T>{
    return this.httpClient
    .post<T>(`${url}/${endpoint}`, JSON.stringify(item), this.httpOptions)
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    //error client
    errorMessage = error.error ? error.error[0]?.message : 'Some error occured';

    return throwError(errorMessage ?? 'Something went wrong');
  }
}
