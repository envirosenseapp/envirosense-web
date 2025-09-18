import { signal, Signal } from '@angular/core';
import { Constructor } from './common';
import { catchError, finalize, Observable, of, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorResponse } from '../modules/api';

export interface WithLoading {
    isLoading: Signal<boolean>;
    errorMessage: Signal<ErrorResponse | null>;
    startLoading(): void;
    loadingAndErrorPipe<T>(source$: Observable<T>): Observable<T>;
}

export function withLoading<T extends Constructor>(Base: T) {
    return class extends Base implements WithLoading {
        isLoading = signal(false);
        errorMessage = signal<ErrorResponse | null>(null);

        startLoading(): void {
            this.isLoading.set(true);
        }

        loadingAndErrorPipe<T>(source$: Observable<T>): Observable<T> {
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
    };
}
