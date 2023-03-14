import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  login<T>(url: string, endpoint: string, item: any) {
    return this.httpClient
      .post<T>(`${url}/${endpoint}`, JSON.stringify(item), this.httpOptions);
  }

  post<T>(url: string, endpoint: string, item: any): Observable<T> {
    return this.httpClient
      .post<T>(`${url}/${endpoint}`, JSON.stringify(item), this.httpOptions);
  }

