import {Component, inject, OnInit} from '@angular/core';
import {ComponentWithPagedData} from '../../../core/component-with-paged-data';
import {Device, DeviceFilters, DeviceService, Measurement, MeasurementFilters} from '../../api';
import {loadingAndErrorPipe, tapLoadingPipe, writeResultPipe} from '../../../core';
import {ActivatedRoute} from '@angular/router';
import {switchMap, takeUntil, tap} from 'rxjs';
import {MeasurementsService} from '../../api/services/measurements.service';
import {NzTableQueryParams} from 'ng-zorro-antd/table';

@Component({
  selector: 'app-view-device-measurements',
  templateUrl: './view-device-measurements.component.html',
  standalone: false,
})
export class ViewDeviceMeasurementsComponent extends ComponentWithPagedData<MeasurementFilters, Measurement> implements OnInit {
  private readonly measurementsService = inject(MeasurementsService);
  private readonly activatedRoute = inject(ActivatedRoute)

  public ngOnInit() {
    this.activatedRoute.paramMap
      .pipe(
        takeUntil(this.onDestroy$)
      )
      .subscribe(params => this.patchFilters({
        pageIndex: 0,
        pageSize: 20,
        deviceId: params.get('id'),
      }));

    this.filters$.pipe(
      takeUntil(this.onDestroy$),
      tapLoadingPipe(this),
      switchMap(f => this.measurementsService.list(f)),
      loadingAndErrorPipe(this),
      writeResultPipe(this),
    )
      .subscribe(v => this.setData(v))
  }

  public onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;

    this.patchFilters({
      pageSize: pageSize,
      pageIndex: pageIndex - 1,
    })
  }
}
