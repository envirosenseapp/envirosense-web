import { signal, Signal } from '@angular/core';
import { Constructor } from './common';
import { catchError, finalize, Observable, of, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import {EmptyPagedApiResult, ErrorResponse, PagedApiResult} from '../modules/api';
import {WithLoading} from './with-loading';
import {toObservable} from '@angular/core/rxjs-interop';

export interface WithPaginationData<TFilters,TModel> {
  filters: Signal<TFilters>;
  filters$: Observable<TFilters>;
  data: Signal<PagedApiResult<TModel>>;
}

export function withPaginationData<T extends Constructor,TFilters, TModel>(Base: T) {
    return class extends Base implements WithPaginationData<TFilters, TModel> {
      filters  = signal<TFilters>({} as TFilters);
      filters$ = toObservable(this.filters);
      data  = signal<PagedApiResult<TModel>>(EmptyPagedApiResult<TModel>());

      public setFilters(filters: TFilters) {
        this.filters.set(filters);
      }

      public setData(data: PagedApiResult<TModel>) {
        this.data.set(data);
      }
    };
}
