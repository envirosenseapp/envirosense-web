import {BaseComponent} from './base-component';
import {Component, signal} from '@angular/core';
import {ErrorResponse} from '../modules/api';
import {Observable, tap} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  template: ''
})
export abstract class ComponentWithLoading extends BaseComponent {
  public isLoading = signal(false);
  public errorMessage = signal<ErrorResponse | null>(null);

  public startLoading(): void {
    this.isLoading.set(true);
  }

 public loadingAndErrorPipe<T>(source$: Observable<T>): Observable<T> {
    return source$.pipe(
      tap({
        next: () => {
          this.isLoading.set(false);
        },
        error: (error:HttpErrorResponse) => {
          this.errorMessage.set(error.error as ErrorResponse);
          this.isLoading.set(false);
        },
        complete: () => {
          this.isLoading.set(false);
        }
      })
    )
  }
}
