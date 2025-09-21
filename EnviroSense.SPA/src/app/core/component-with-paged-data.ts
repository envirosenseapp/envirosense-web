import {Component, Directive, OnInit, signal} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {EmptyPagedApiResult, PagedApiResult} from '../modules/api';
import {ComponentWithLoading} from './component-with-loading';

@Directive()
export abstract class ComponentWithPagedData<TFilters, TModel> extends ComponentWithLoading {
  protected filters = signal<TFilters>({} as TFilters);
  protected filters$ = toObservable(this.filters);
  protected data = signal<PagedApiResult<TModel>>(EmptyPagedApiResult<TModel>());

  public setFilters(filters: TFilters) {
    this.filters.set(filters);
  }

  public setData(data: PagedApiResult<TModel>) {
    this.data.set(data);
  }

  public setPage(pageIndex: number): void {
    this.setFilters({
      ...this.filters(),
      pageIndex: pageIndex
    })
  }

  public setPageSize(pageSize: number): void {
    this.setFilters({
      ...this.filters(),
      pageSize: pageSize
    })
  }
}
