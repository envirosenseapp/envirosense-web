import {HttpErrorResponse} from '@angular/common/http';
import {ComponentWithLoading} from './component-with-loading';
import {Observable, tap} from 'rxjs';
import {ErrorResponse} from '../modules/api';

export function loadingAndErrorPipe<T extends ComponentWithLoading, T1>(cmp: T): (source$: Observable<T1>) => Observable<T1> {
  return source$ => source$.pipe(
    tap({
      next: () => {
        cmp.stopLoading();
      },
      error: (error: HttpErrorResponse) => {
        cmp.setError(error.error as ErrorResponse);
        cmp.stopLoading();
      },
      complete: () => {
        cmp.stopLoading();
      }
    })
  )
}
