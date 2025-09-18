import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ApiHttpClient {
  constructor(private readonly http: HttpClient) {}

  get<T>(url: string, queryParams: any | null): Observable<T> {
    return this.http.get<T>(url, {
      params: queryParams,
    });
  }

  post<T>(url: string, body: unknown): Observable<T> {
    return this.http.post<T>(url, body);
  }
}
