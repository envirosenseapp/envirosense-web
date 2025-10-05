import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {SessionService} from '../services';

@Injectable({ providedIn: 'root' })
export class ApiHttpClient {
  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly sessionService: SessionService,
  ) {}

  get<T>(url: string, queryParams?: any): Observable<T> {
    return this.http.get<T>(url, {
      params: queryParams,
    }).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  post<T>(url: string, body: unknown): Observable<T> {
    return this.http.post<T>(url, body).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 401) {
      this.sessionService.clear();
      this.router.navigate(['/sign-in']);
    }
    return throwError(() => error);
  }
}
