import {Component, OnInit, signal, WritableSignal, inject} from '@angular/core';
import {DeviceService, Device, DeviceFilters} from '../../api';
import {switchMap, takeUntil, tap} from 'rxjs';
import {ComponentWithPagedData} from '../../../core/component-with-paged-data';

@Component({
  templateUrl: './dashboard.component.html',
  standalone: false,
})
export class DashboardComponent extends ComponentWithPagedData<DeviceFilters, Device> implements OnInit {
  private readonly deviceService = inject(DeviceService);

  public now = new Date();

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.filters$.pipe(
      takeUntil(this.onDestroy$),
      tap(() => this.startLoading()),
      switchMap(filters => this.deviceService.list(filters)),
      this.loadingAndErrorPipe.bind(this),
    )
      .subscribe(this.setData.bind(this));

    this.setFilters({
      pageIndex: 0,
      pageSize: 5,
    } as DeviceFilters)
  }
}
