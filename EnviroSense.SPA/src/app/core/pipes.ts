import {HttpErrorResponse} from '@angular/common/http';
import {ComponentWithLoading} from './component-with-loading';
import {Observable, tap} from 'rxjs';
import {BaseFilters, ErrorResponse, PagedApiResult} from '../modules/api';
import {ComponentWithPagedData} from './component-with-paged-data';

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

export function writeResultPipe<T extends ComponentWithPagedData<T2, T3>, T2 extends BaseFilters, T3>(cmp: T): (source$: Observable<PagedApiResult<T3>>) => Observable<PagedApiResult<T3>> {
  return source$ => source$.pipe(
    tap({
      next: (data) => {
        cmp.setData(data);
      },
    })
  )
}

export function tapLoadingPipe<T extends ComponentWithLoading, T1>(cmp: T): (source$: Observable<T1>) => Observable<T1> {
  return source$ => source$.pipe(
    tap({
      next: () => {
        cmp.startLoading();
      },
    })
  )
}
