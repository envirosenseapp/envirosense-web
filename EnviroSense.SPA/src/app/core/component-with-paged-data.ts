import {Component, Directive, OnInit, signal} from '@angular/core';
import {toObservable} from '@angular/core/rxjs-interop';
import {BaseFilters, EmptyPagedApiResult, PagedApiResult} from '../modules/api';
import {ComponentWithLoading} from './component-with-loading';

@Directive()
export abstract class ComponentWithPagedData<TFilters extends BaseFilters, TModel> extends ComponentWithLoading {
  protected filters = signal<TFilters>({} as TFilters);
  protected filters$ = toObservable(this.filters);
  protected data = signal<PagedApiResult<TModel>>(EmptyPagedApiResult<TModel>());

  public setFilters(filters: TFilters) {
    this.filters.set(filters);
  }

  public patchFilters(filters: Partial<TFilters>){
    this.filters.set({
      ...this.filters(),
      ...filters,
    } as TFilters    );
  }

  public setData(data: PagedApiResult<TModel>) {
    this.data.set(data);
  }

  public setPage(pageIndex: number): void {
    this.patchFilters({
      pageIndex: pageIndex
    } as TFilters)
  }

  public setPageSize(pageSize: number): void {
    this.patchFilters({
      pageSize: pageSize
    } as TFilters)
  }
}
