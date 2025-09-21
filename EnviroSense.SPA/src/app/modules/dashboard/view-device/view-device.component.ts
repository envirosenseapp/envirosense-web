import {Component, inject, OnInit, signal, Signal} from '@angular/core';
import {ComponentWithLoading, loadingAndErrorPipe} from '../../../core';
import {ActivatedRoute} from '@angular/router';
import {takeUntil} from 'rxjs';
import {Device, DeviceService} from '../../api';

@Component({
  selector: 'app-view-device',
  templateUrl: './view-device.component.html',
  standalone: false,
})
export class ViewDeviceComponent extends ComponentWithLoading implements OnInit {
  private activatedRoute = inject(ActivatedRoute)
  private deviceService = inject(DeviceService);

  device = signal<Device | null>(null);

  public ngOnInit() {
    this.activatedRoute.paramMap
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(params => this.loadDevice(params.get('id')));
  }

  private loadDevice(id: string | null) {
    if (id == null) {
      return;
    }

    this.startLoading();
    this.deviceService.getById(id)
      .pipe(
        loadingAndErrorPipe(this),
      )
      .subscribe(
        {
          next: d => this.device.set(d),
          error: err => this.device.set(null),
        }
      );
  }
}
