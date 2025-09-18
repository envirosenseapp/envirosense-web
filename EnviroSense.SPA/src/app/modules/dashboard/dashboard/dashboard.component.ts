import {Component, OnInit, signal, WritableSignal, inject} from '@angular/core';
import {DeviceService, Device, DeviceFilters} from '../../api';
import {Constructor, Empty, WithLoading, withLoading, withPaginationData} from '../../../mixin';
import {toObservable} from '@angular/core/rxjs-interop';
import {switchMap, tap} from 'rxjs';

@Component({
  templateUrl: './dashboard.component.html',
  standalone: false,
})
export class DashboardComponent extends withPaginationData<Constructor<WithLoading>, DeviceFilters, Device>(withLoading(Empty)) implements OnInit {
  private readonly deviceService = inject(DeviceService);

  public now = new Date();

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.filters$.pipe(
      tap(() => this.startLoading()),
      switchMap(filters => this.deviceService.list(filters)),
      this.loadingAndErrorPipe.bind(this)
    )
      .subscribe(this.setData.bind(this));

    this.setFilters({
      pageIndex: 0,
      pageSize: 5,
    } as DeviceFilters)

  }

  // TODO: move to mixin
  setPage(pageIndex: number): void {
    this.setFilters({
      ...this.filters(),
      pageIndex: pageIndex
    })
  }

  // TODO: move to mixin
  setPageSize(pageSize: number): void {
    this.setFilters({
      ...this.filters(),
      pageSize: pageSize
    })
  }
}
